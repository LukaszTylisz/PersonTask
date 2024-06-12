<template>
	<div class="container text-white">
    <div class="d-flex justify-content-left">
		<nav class="navbar navbar-custom bg-dark mt-3 mb-5 py-2 px-5 rounded">
			<div class="container-fluid">
				<a class="navbar-brand" href="/">Refresh Data</a>
			</div>
		</nav>
  </div>
		<router-view />
	</div>
  <br>
  <br>
</template>

<script>
export default {
	name: 'app',
}
</script>

<style>
@import './assets/main.css';
</style>