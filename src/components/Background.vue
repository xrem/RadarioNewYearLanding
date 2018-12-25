<template>
    <div class="background">
        <div v-if="covers.length > 0" class="background-cover" v-for="cover in covers" :style="{'background-image': `url(${cover})`}"></div>
        <div class="background-pattern" v-else>
            <div class="background-pattern__child"></div>
            <div class="background-pattern__column">
                <div class="background-pattern__child"></div>
                <div class="background-pattern__child"></div>
            </div>
        </div>
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
        filter: brightness(80%);
    }

    .background-cover {
        min-width: calc(100% / 4 - 20px);
        min-height: calc(100% / 2 - 20px);
        border: 10px solid #fff;
        background-position: center;
        background-size: cover;
    }

    .background-pattern {
        display: flex;
        flex-shrink: 0;
        width: 100%;
        height: 100%;
    }

    .background-pattern__column{
        width: 50%;
        height: 100%;
        display: flex;
        flex-direction: column;
    }

    .background-pattern__child:nth-child(1) {
        background: #5858e7;
        width: 50%;
        height: 100%;
        flex: 1;
    }

    .background-pattern__column .background-pattern__child:nth-child(1) {
        background: #f66646;
        width: 100%;
        height: 50%;
    }

    .background-pattern__column .background-pattern__child:nth-child(2){
        background: #facb50;
        width: 100%;
        height: 50%;
    }
</style>