<template>
  <div class="masonry-with-columns pt-3" v-if="keeps.length > 0">
    <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'

export default {
  name: 'Home',
  setup() {
    onMounted(() => {
      keepsService.getAllKeeps()
    })
    return {
      keeps: computed(() => AppState.keeps)

    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }

}

</style>
