<template>
    <div class="background">
        <div class="background-cover"></div>
        <div class="background-cover"></div>
        <div class="background-cover"></div>
        <div class="background-cover"></div>
        <div class="background-cover"></div>
        <div class="background-cover"></div>
        <div class="background-cover"></div>
        <div class="background-cover"></div>
    </div>
</template>

<script>
    export default {
        name: "Background"
    }
</script>

<style scoped>
    .background {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100vh;
        box-sizing: border-box;
        border: 45px solid #fff;
        z-index: 0;
        display: flex;
        flex-wrap: wrap;
        filter: brightness(95%);
    }

    .background-cover {
        width: calc(100% / 4 - 20px);
        height: calc(100% / 2 - 20px);
        border: 10px solid #fff;
        background-size: cover;
    }

    .background-cover:nth-child(1) {
        background: red;
    }

    .background-cover:nth-child(2) {
        background: blue;
    }

    .background-cover:nth-child(3) {
        background: yellow;
    }

    .background-cover:nth-child(4) {
        background: orange;
    }

    .background-cover:nth-child(5) {
        background: greenyellow;
    }

    .background-cover:nth-child(6) {
        background: violet;
    }

    .background-cover:nth-child(7) {
        background: grey;
    }

    .background-cover:nth-child(8) {
        background: deepskyblue;
    }
</style>