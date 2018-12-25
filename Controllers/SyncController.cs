using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.DAL.RadarioSQL;
using NewYearLanding.DAL.RadarioSQL.DTO;
using NewYearLanding.Model;

namespace NewYearLanding.Controllers {
    [Authorize]
    [Route("[controller]")]
    public class SyncController : BaseController {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILogger _logger;

        private const string ImageServerBaseUrl = "https://images.radario.ru/images/";
        private const string HostLogoPostfix = "webhostpageimage/";

        private readonly CultureInfo _ruLocale = new CultureInfo("ru-RU");

        public SyncController(ICompaniesRepository companiesRepository,
                              IStatisticsRepository statisticsRepository,
                              ILogger<SyncController> logger,
                              IModelFacade modelFacade) : base(modelFacade) {
            _companiesRepository = companiesRepository;
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("companies")]
        public async Task<JsonResult> SyncCompanies() {
            var knownCompanies = await _companiesRepository.GetAllCompanies();
            var radarioCompanies = mf.CompanyManager.GetAllCompanies();
            var updateCount = 0;
            var insertCount = 0;
            foreach (var radarioCompany in radarioCompanies) {
                var entity = knownCompanies.SingleOrDefault(x => x.CompanyId == radarioCompany.Id);
                if (entity == null) {
                    entity = new Company() {
                        CompanyId = radarioCompany.Id,
                        CompanyTitle = radarioCompany.Title,
                        PublicGuid = Guid.NewGuid(),
                        FullAccessGuid = Guid.NewGuid()
                    };
                    await _companiesRepository.Insert(entity);
                    insertCount++;
                } else {
                    if (!radarioCompany.Title.Equals(entity.CompanyTitle,
                        StringComparison.InvariantCultureIgnoreCase)) {
                        entity.CompanyTitle = radarioCompany.Title;
                        await _companiesRepository.Update(entity);
                        updateCount++;
                    }
                }
            }
            return Json(new {
                Updated = updateCount,
                CreatedNew = insertCount
            }, JsonSerializerSettings);
        }

        [HttpGet]
        [Route("stats")]
        public async Task<JsonResult> SyncStatistics(int? from) {
            var stats = await _statisticsRepository.GetAllCompaniesStatistics();
            var radarioCompanies = mf.CompanyManager.GetAllCompanies();
            var updateCount = 0;
            var insertCount = 0;
            foreach (var radarioCompany in radarioCompanies.Where(x => x.Id > from.GetValueOrDefault())) {
                var entity = stats.SingleOrDefault(x => x.CompanyId == radarioCompany.Id);
                var shouldInsert = entity == null;
                entity = entity ?? new Statistics();
                entity.Hostname = radarioCompany.Title;
                entity.CompanyId = radarioCompany.Id;
                entity.Logo = GetLogo(radarioCompany.Logo);
                var aggregatedYearInfo = mf.CompanyManager.GetAggregatedYearInfoByCompany(radarioCompany.Id);
                if (aggregatedYearInfo.Any()) {
                    entity.Year = MapYearInfo(aggregatedYearInfo);
                    var eventIdsWithSales = aggregatedYearInfo.Select(x => x.EventId);
                    entity.EventsCovers = ProcessEventCovers(eventIdsWithSales);
                    entity.BestChannel = GetBestChannels(radarioCompany.Id);
                    var dateStats = mf.CompanyManager.GetAggregatedDateStatistics(radarioCompany.Id);
                    entity.BestMonth = GetBestMonth(dateStats);
                    entity.BestDay = GetBestDayOfWeek(dateStats);
                    entity.BestTime = GetBestTime(dateStats);
                } else {
                    _logger.LogInformation($"Host {radarioCompany.Id} {radarioCompany.Title} have sold nothing in 2018");
                }
                if (shouldInsert) {
                    await _statisticsRepository.Insert(entity);
                    insertCount++;
                } else {
                    await _statisticsRepository.Update(entity);
                    updateCount++;
                }

                if (updateCount % 10 == 0 || insertCount % 10 == 0) {
                    _logger.LogInformation($"[SyncStatistics] Already updated: {updateCount} | inserted: {insertCount}");
                }
            }
            return Json(new {
                Updated = updateCount,
                CreatedNew = insertCount
            }, JsonSerializerSettings);
        }

        private string GetBestTime(List<AggregatedDateStatistics> dateStats) {
            try {
                var times = dateStats.GroupBy(x => x.Hour).ToDictionary(x => x.Key, x => x.Sum(z => z.Weight));
                var maxValue = times.Values.Max();
                var time = times.First(x => x.Value == maxValue).Key;
                return $"{time}:00";
            } catch {
                _logger.LogInformation("Problems while trying to get best month");
                return "-";
            }
        }

        private string GetBestDayOfWeek(List<AggregatedDateStatistics> dateStats) {
            try {
                var weeks = dateStats.GroupBy(x => {
                        var date = new DateTime(2018, x.Month, x.Day, x.Hour, 0, 0);
                        return date.ToString("dddd", _ruLocale);
                    }).ToDictionary(x => x.Key, x => x.Sum(z => z.Weight));
                var maxValue = weeks.Values.Max();
                var week = weeks.First(x => x.Value == maxValue).Key;
                return week;
            } catch {
                _logger.LogInformation("Problems while trying to get best day of week");
                return "-";
            }
        }

        private string GetBestMonth(List<AggregatedDateStatistics> dateStats) {
            try {
                var months = dateStats.GroupBy(x => x.Month).ToDictionary(x => x.Key, x => x.Sum(z => z.Weight));
                var maxValue = months.Values.Max();
                var month = months.First(x => x.Value == maxValue).Key;
                return _ruLocale.DateTimeFormat.GetMonthName(month);
            } catch {
                _logger.LogInformation("Problems while trying to get best month");
                return "-";
            }
        }

        private string GetBestChannels(int radarioCompanyId) {
            const string threeChannelsFormat = "{0}, {1} и {2}";
            const string twoChannelsFormat = "{0} и {1}";
            var channels = mf.CompanyManager
                             .GetTop3CompanyDistributionChannels(radarioCompanyId)
                             .Select(MapChannels)
                             .ToList();
            switch (channels.Count) {
                case 3:
                    return string.Format(threeChannelsFormat, channels[0], channels[1], channels[2]);
                case 2:
                    return string.Format(twoChannelsFormat, channels[0], channels[1]);
                case 1:
                    return channels[0];
            }

            return null;
        }

        private static string MapChannels(string arg) {
            switch (arg) {
                case "RADARIO_WEBSITE":
                    return "Веб-сайт Радарио";
                case "VK_APP":
                    return "Приложение для ВКонтакте";
                case "WIDGET":
                    return "Виджет";
                case "MOBILE_WIDGET":
                    return "Мобильный виджет";
                case "BOOKING_OFFICE":
                    return "Касса";
                case "FB_APP":
                    return "Приложение для Facebook";
                case "MOBILE_APP":
                    return "Мобильное приложение Radario";
                case "API":
                    return "API";
                case "FIVE_MINUTES_SITE":
                    return "Сайт пятиминутка";
                case "COMPANY_CONTRACTOR":
                    return "Распространители";
                case "TICKET_DESK":
                    return "Билетный стол";
                case "OK_APP":
                    return "Приложение для Одноклассников";
                case "DELIVERY":
                    return "Доставка";
            }

            return arg;
        }

        private List<string> ProcessEventCovers(IEnumerable<int> eventIdsWithSales) {
            var images = mf.EventManager.GetImagesByEventIds(eventIdsWithSales);
            var covers = images.GroupBy(x => x.Title).Select(x => {
                    try {
                        var logos = JsonConvert.DeserializeObject<List<LogoWithCover>>(x.First().Images);
                        var cover = logos.SingleOrDefault(z => z.Cover);
                        return cover?.Id;
                    } catch {
                        return null;
                    }
                })
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => ImageServerBaseUrl + x);
            return covers.Take(8).ToList();
        }

        private StatisticsYear MapYearInfo(List<AggregatedYearInfo> aggregatedYearInfo) {
            return new StatisticsYear {
                Events = aggregatedYearInfo.Count,
                Money = aggregatedYearInfo.Sum(x => x.GrossWithDiscount),
                Tickets = aggregatedYearInfo.Sum(x => x.TicketCount)
            };
        }

        private static string GetLogo(string companyLogo) {
            string logo = null;
            if (!string.IsNullOrWhiteSpace(companyLogo)) {
                try {
                    var dto = JsonConvert.DeserializeObject<Logo>(companyLogo);
                    logo = ImageServerBaseUrl + HostLogoPostfix + dto.Id;
                } catch { }
            }
            return logo;
        }
    }
}