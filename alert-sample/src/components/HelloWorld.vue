<template>
  <div class="alert">{{ message }}</div>
</template>

<script lang="ts">
import Vue from 'vue';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';

const connection = new HubConnectionBuilder()
  .withUrl('http://localhost:7071/api/')
  .configureLogging(LogLevel.Information)
  .build();

interface Data {
  value: number;
}

export default Vue.extend({
  data() {
    return {
      message: 'No error',
    };
  },
  methods: {
    onAlert(data: Data) {
      this.message = `${new Date()}: ${data.value}`;
    },
  },
  async created() {
    await connection.on('onAlert', (data: Data) => {
      this.onAlert(data);
    });
    await connection.start();
  },
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.alert {
  font-size: xx-large;
  color: red;
}
</style>
