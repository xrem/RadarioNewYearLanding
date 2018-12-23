<template>
    <div class="footer">
        <div class="icon"></div>
        <p class="subtitle">Спасибо, что вы с нами! Мы благодарны за доверие и будем<br>
            делать сервис еще лучше. Вместе нас ждут великие дела.</p>
        <a href="" class="btn">Подробная статистика</a>
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

    .subtitle {
        margin-bottom: 40px;
    }

    @media (max-width: 900px) {
        .footer {
            padding: 60px 20px;
        }
    }
</style>