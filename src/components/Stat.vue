<template>
    <div class="stat" :class="{'stat_gray' : channel}">
        <div class="head" v-if="year">за 2018 год</div>
        <div class="row" :class="{'row-3': year}" v-if="year">
            <div class="col">
                <div class="title">{{formatPrice(year.events)}}</div>
                <div class="subtitle">событий организовали </div>
            </div>
            <div class="col">
                <div class="title">{{formatPrice(year.tickets)}}</div>
                <div class="subtitle">билетов продали </div>
            </div>
            <div class="col">
                <div class="title">{{formatPrice(year.money)}} ₽</div>
                <div class="subtitle">денег заработали</div>
            </div>
        </div>

        <div class="row" v-if="month">
            <div class="col">
                <div class="title title_cap">{{month}} <img :src="monthImg(month)" alt=""></div>
                <div class="subtitle">был самым ударным месяцем по продаже билетов</div>
            </div>
        </div>

        <div class="row" v-if="time">
            <div class="col">
                <div class="title">{{time}} <img :src="timeImg(time)" alt=""></div>
                <div class="subtitle">“час пик” для продаж ваших билетов</div>
            </div>
        </div>

        <div class="row" v-if="day">
            <div class="col">
                <div class="title title_cap">{{day}} <img :src="dayImg(day)" alt=""></div>
                <div class="subtitle">в этот день покупали чаще всего</div>
            </div>
        </div>

        <div class="row" v-if="channel">
            <div class="col">
                <div class="title">{{channel}}</div>
                <div class="subtitle">стали лучшими каналами продаж </div>
            </div>
        </div>
    </div>
</template>

<script>
    import winter from '../assets/seasons/winter.png';
    import spring from '../assets/seasons/spring.png';
    import summer from '../assets/seasons/summer.png';
    import fall from '../assets/seasons/fall.png';
    import one from '../assets/time/1-13.png';
    import two from '../assets/time/2-14.png';
    import three from '../assets/time/3-15.png';
    import four from '../assets/time/4-16.png';
    import five from '../assets/time/5-17.png';
    import six from '../assets/time/6-18.png';
    import seven from '../assets/time/7-19.png';
    import eight from '../assets/time/8-20.png';
    import nine from '../assets/time/9-21.png';
    import ten from '../assets/time/10-22.png';
    import eleven from '../assets/time/11-23.png';
    import twelve from '../assets/time/12-00.png';
    import mon from '../assets/days/mon.png';
    import tue from '../assets/days/tue.png';
    import wed from '../assets/days/wed.png';
    import thu from '../assets/days/thu.png';
    import fri from '../assets/days/fri.png';
    import sun from '../assets/days/sun.png';
    import sat from '../assets/days/sat.png';

    export default {
        name: "Stat",
        data() {
            return {
                seasons: {
                    'январь': winter,
                    'февраль': winter,
                    'март': spring,
                    'апрель': spring,
                    'май': spring,
                    'июнь': summer,
                    'июль': summer,
                    'август': summer,
                    'сентябрь': fall,
                    'октябрь': fall,
                    'ноябрь': fall,
                    'декабрь': winter,
                },
                hours: {
                    '1': one,
                    '13': one,
                    '2': two,
                    '14': two,
                    '3': three,
                    '15': three,
                    '4': four,
                    '16': four,
                    '5': five,
                    '17': five,
                    '6': six,
                    '18': six,
                    '7': seven,
                    '19': seven,
                    '8': eight,
                    '20': eight,
                    '9': nine,
                    '21': nine,
                    '10': ten,
                    '22': ten,
                    '11': eleven,
                    '23': eleven,
                    '12': twelve,
                    '0': twelve,
                },
                days: {
                    'понедельник': mon,
                    'вторник': tue,
                    'среда': wed,
                    'четверг': thu,
                    'пятница': fri,
                    'суббота': sun,
                    'воскресеньк': sat,
                }
            }
        },
        props: {
            year: {
                type: Object
            },
            month: {
                type: String
            },
            time: {
                type: String
            },
            day: {
                type: String
            },
            channel: {
                type: String
            }
        },
        methods: {
            formatPrice(value) {
                let val = (value/1);
                return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")
            },
            monthImg: function(month) {
                return this.seasons[month.toLowerCase()];
            },
            timeImg: function(time) {
                return this.hours[time.split(':')[0]];
            },
            dayImg: function(day) {
                return this.days[day.toLowerCase()];
            }

        }
    }
</script>

<style scoped>
    .stat {
        background: #fff;
        padding: 150px 30px;
        text-align: center;
    }
    .stat_gray {
        background: #f1f3f6;
    }

    .head {
        color: #000000;
        opacity: 0.5;
        font-size: 22px;
        font-weight: 400;
        line-height: 30px;
        margin-bottom: 70px;
    }

    .row {
        display: flex;
        justify-content: space-around;
        flex-wrap: wrap;
    }
    .row-3 .col {
        width: 300px;
        margin-bottom: 60px;
    }

    .title {
        margin-bottom: 25px;
    }

    @media (max-width: 900px) {
        .title {
            white-space: normal;
            margin-bottom: 10px;
        }

        .stat {
            padding: 60px 20px;
        }
    }
</style>