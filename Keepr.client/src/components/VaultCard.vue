<template>
  <div class="vault-container grow hoverable" @click="goToVault(vault.id)">
    <img src="../assets/img/orange.png" alt="" height="200">
    <div class="content">
      <p>{{ vault.name }}</p>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    return {
      goToVault() {
        return 'not implemented'
      }
    }
  }
}
</script>

<style>
.vault-container{
  position: relative;
  text-align: center;
  padding: 0.25rem;
}
.vault-container .content{
  position: absolute;
  bottom: 0;
  text-shadow: black;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #0c0c0c;
  width: 100%;
  padding: 20px;
  font-size: 1.2rem;
}
</style>
