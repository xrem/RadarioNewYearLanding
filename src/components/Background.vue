<template>
    <div class="background">
        <div class="background-cover" v-for="cover in covers" :style="{'background-image': `url(${cover})`}"></div>
    </div>
</template>

<script>
    export default {
        name: "Background",
        props: {
            covers: {
                type: Array
            }
        }
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
        min-width: calc(100% / 4 - 20px);
        min-height: calc(100% / 2 - 20px);
        border: 10px solid #fff;
        background-size: cover;
    }
</style>