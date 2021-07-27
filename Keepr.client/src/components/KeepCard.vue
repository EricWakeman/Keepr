<template>
  <div>
    <img :src="keep.img"
         :alt="keep.name"
         class="keep-img hoverable grow"
         @click="setActiveKeep()"
         data-toggle="modal"
         data-target=".bd-example-modal-lg"
    >
  </div>
</template>

<script>
import { keepsService } from '../services/KeepsService'
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      setActiveKeep() {
        keepsService.updateKeepViews(props.keep)
      }
    }
  }
}
</script>

<style>

.keep-img{
  max-width: 100%;
}
.hoverable{
  cursor: pointer;
}
.grow{
  transition: all .25s linear;
}
.grow:hover{
  transform: scale(1.1);
}
</style>
