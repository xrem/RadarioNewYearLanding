<template>
    <div class="cover">
        <div class="logo">
            <span class="logo-img"></span>
            <span class="logo-cross" v-if="host.logo !== ''"></span>
            <img v-if="host.logo !== ''" :src="host.logo" :alt="host.hostname" class="logo-host">
        </div>
        <p class="title">
            {{ this.host.hostname }}! Поздравляем вас<br>
            с наступающим 2019 годом!
        </p>
        <p class="subtitle">
            Самое время подводить итоги и строить планы на будущее.<br>
            Вот статистика, как мы с вами собирали залы в этом году.
        </p>
        <a href="#" v-scroll-to="'#stat-1'" class="btn">Посмотреть</a>
    </div>
</template>

<script>
    export default {
        name: "Cover",
        props: {
            host: {
                type: Object
            }
        }
    }
</script>

<style scoped>
    .cover {
        padding: 200px 30px 120px;
        text-align: center;
        border-radius: 10px 10px 0 0;
        background: #e7e9ee;
        animation: fire 2s ease-in-out forwards;
        animation-delay: 2s;
    }

    .logo {
        height: 62px;
        margin-bottom: 40px;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .logo-img {
        width: 138px;
        height: 33px;
        background: url('../assets/logo.svg') center no-repeat;
    }
    .logo-cross {
        width: 15px;
        height: 15px;
        margin: 0 10px -10px;
        background: url('../assets/cross.svg') center no-repeat;
    }
    .logo-host {
        max-height: 33px;
        object-fit: contain;
        margin-bottom: -10px;
        border-radius: 50%;
    }

    .title {
        margin-bottom: 30px;
    }

    .subtitle {
        opacity: 0.7;
        color: #000;
        font-size: 20px;
        font-weight: 400;
        line-height: 30px;
        margin-bottom: 50px;
    }

    @keyframes fire {
        0% {
            background: #e7e9ee url('../assets/confetty.svg') 50% calc(100% + 50px);
            background-repeat: no-repeat;
            background-size: 10px;
        }
        40% {
            background: #e7e9ee url('../assets/confetty.svg') 50% 80%;
            background-repeat: no-repeat;
            background-size: 50px;
        }
        100% {
            background: #e7e9ee url('../assets/confetty.svg') 50%;
            background-repeat: no-repeat;
            background-size: 100%;
        }
    }

    @media (max-width: 767px) {
        .cover {
            padding: 60px 20px;
        }
    }
</style>