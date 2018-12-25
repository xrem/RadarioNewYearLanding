<template>
    <div class="footer">
        <div class="icon"></div>
        <p class="subtitle">Спасибо, что вы с нами! Мы благодарны за доверие и будем<br>
            делать сервис еще лучше. Вместе нас ждут великие дела.</p>
        <a href="https://marketing.radario.co/insights/sales" target="_blank" class="btn">Подробная статистика</a>
        <p class="ps">С любовью, команда Радарио ❤️</p>
    </div>
</template>

<script>
    export default {
        name: "Footer"
    }
</script>

<style scoped>
    .footer {
        text-align: center;
        padding:  150px 30px;
        border-radius: 0 0 10px 10px;
    }

    .icon {
        margin: 0 auto 30px;
        width: 47px;
        height: 60px;
        background: url('../assets/christmas-tree.png') center no-repeat;
        background-size: cover;
    }

    .btn{
        margin-bottom: 30px;
    }

    .ps {
        color: #2d2a26;
        font-size: 18px;
        font-weight: 400;
        line-height: 30px;
        text-align: center;
    }

    .subtitle {
        margin-bottom: 40px;
    }

    @media (max-width: 900px) {
        .footer {
            padding: 60px 20px;
        }
    }
</style>