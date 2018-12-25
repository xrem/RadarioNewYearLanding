<template>
    <div id="app">
        <Background :covers="host.eventsCovers"/>
        <div class="container">
            <Cover :host="host"/>
            <Stat :year="host.year" id="stat-1"/>
            <hr>
            <Stat :month="host.bestMonth"/>
            <hr>
            <Stat :time="host.bestTime"/>
            <hr>
            <Stat :day="host.bestDay"/>
            <Stat :channel="host.bestChannel"/>
            <Footer/>
        </div>
    </div>
</template>

<script>

    import Cover from "./components/Cover";
    import Stat from "./components/Stat";
    import Footer from "./components/Footer";
    import Background from "./components/Background";
    import axios from 'axios';

    export default {
        name: 'app',
        components: {
            Background,
            Footer,
            Stat,
            Cover

        },
        data() {
            return {
                host: {
                    hostname: '',
                    logo: '',
                    eventsCovers: [],
                    year: {
                        events: null,
                        tickets: null,
                        money: null
                    },
                    bestMonth: '',
                    bestTime: '',
                    bestDay: '',
                    bestChannel: ''
                }
            }
        },
        created() {
            axios.post(`/${window.location.pathname.replace('/', '')}/`)
                .then(res => {
                    this.host = res.data.data;
                })
                .catch(err => {
                    console.error(err)
                })
        }
    }
</script>

<style>
    @import url('https://fonts.googleapis.com/css?family=Roboto:400,700');

    body {
        font-family: 'Roboto', sans-serif;
    }

    button {
        border: none;
        box-shadow: none;
    }

    hr {
        border: none;
        border-bottom: 1px solid #e5e5e5;
        width: 80%;
    }

    .container {
        max-width: 1200px;
        margin: 280px auto;
        background: #fff;
        border-radius: 10px;
        box-shadow: 0 30px 100px rgba(0, 0, 0, 0.3);
        position: relative;
        z-index: 1;
    }

    .title {
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 40px;
        line-height: 50px;
        font-weight: 700;
        color: #111;
        margin-bottom: 30px;
    }
    .title img {
        height: 40px;
        margin-left: 20px;
    }
    .title_cap {
        text-transform: capitalize;
    }
    .subtitle {
        opacity: 0.8;
        color: #000000;
        font-size: 22px;
        font-weight: 400;
        line-height: 30px;
    }

    .btn {
        display: inline-block;
        text-decoration: none;
        color: #fff;
        font-size: 18px;
        font-weight: 500;
        line-height: 50px;
        border-radius: 25px;
        background: #466eff;
        padding: 0 32px;
        cursor: pointer;
        transition: background .2s;
    }
    .btn:hover {
        background: #0b4fff;
    }

    @media (max-width: 767px) {
        .container {
            margin: 20px auto;
        }

        .title {
            font-size: 26px;
            line-height: 34px;
        }

        .subtitle {
            font-size: 18px;
            line-height: 26px;
        }
    }
</style>
