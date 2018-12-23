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
                <div class="title">{{month}}</div>
                <div class="subtitle">был самым ударным месяцем по продаже билетов</div>
            </div>
        </div>

        <div class="row" v-if="time">
            <div class="col">
                <div class="title">{{time}}</div>
                <div class="subtitle">“час пик” для продаж ваших билетов</div>
            </div>
        </div>

        <div class="row" v-if="day">
            <div class="col">
                <div class="title">{{day}}</div>
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
    export default {
        name: "Stat",
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
        white-space: nowrap;
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