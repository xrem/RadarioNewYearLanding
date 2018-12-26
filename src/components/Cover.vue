<template>
    <div class="cover">
        <div class="confetti"></div>
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
        position: relative;
        overflow: hidden;
    }
    .confetti {
        position: absolute;
        background: url('../assets/confetty.svg');
        background-size: cover;
        width: 50px;
        height: 50px;
        top: 100%;
        left: calc(50% - 25px);
        animation: fire 2s ease-in-out forwards;
        animation-delay: 1s;
        z-index: 0;
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
        position: relative;
        z-index: 1;
    }
    .logo-cross {
        width: 15px;
        height: 15px;
        margin: 0 10px -10px;
        background: url('../assets/cross.svg') center no-repeat;
        position: relative;
        z-index: 1;
    }
    .logo-host {
        max-height: 33px;
        object-fit: contain;
        margin-bottom: -10px;
        border-radius: 50%;
        position: relative;
        z-index: 1;
    }

    .title {
        margin-bottom: 30px;
        position: relative;
        z-index: 1;
    }

    .subtitle {
        opacity: 0.7;
        color: #000;
        font-size: 20px;
        font-weight: 400;
        line-height: 30px;
        margin-bottom: 50px;
        position: relative;
        z-index: 1;
    }

    @keyframes fire {
        0% {
            top: 100%;
        }
        40% {
            top: 20%;
        }
        100% {
            left: 0;
            top: 0;
            height: 100%;
            width: 100%;
        }
    }

    @media (max-width: 767px) {
        .cover {
            padding: 60px 20px;
        }
    }
</style>