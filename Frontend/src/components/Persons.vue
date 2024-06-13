<template>
	<div class="container bg-dark p-5 rounded">
		<h3 class="mb-3 border-bottom-orange pb-3 px-2 text-orange">All Persons</h3>

		<button id="show-modal" class="btn btn-lg mb-4 glow-on-hover" @click="addPerson()">Add Person</button>

		<Teleport to="body">
			<modal :show="showModal" :selected="selectedPerson" :edit="isEditMode" @close="handleModalClose">
				<template #header>
					<h3>{{ modalHeader }}</h3>
				</template>
			</modal>
		</Teleport>

		<div v-if="message" class="alert alert-success">{{ message }}</div>
		<div class="container">
			<table class="table table-dark">
				<thead>
					<tr>
						<th class="text-orange">Id</th>
						<th>First Name</th>
						<th>Last Name</th>
						<th>Description</th>
						<th>Emails</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
					<tr v-for="Person in Persons" v-bind:key="Person.id">
						<td>{{ Person.id }}</td>
						<td>{{ Person.firstName }}</td>
						<td>{{ Person.lastName }}</td>
						<td>{{ Person.description }}</td>
						<td>
							<ul>
								<li v-for="(email, index) in Person.emails" :key="index">
									{{ email.emailAddress }}
								</li>
							</ul>
						</td>
						<td>
						</td>
						<td>
							<div class="btn-group" role="group" aria-label="Basic example">
								<button class="btn btn-info mx-1 text-white"
									v-on:click="editPerson(Person)">Edit</button>
								<button class="btn btn-danger" v-on:click="deletePerson(Person.id)">Delete</button>
							</div>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
	</div>
</template>

<script>
import PersonDataService from '../service/PersonDataService'
import Modal from './PersonModal.vue'

export default {
	name: 'PersonsList',
	data() {
		return {
			Persons: [],
			message: '',
			showModal: false,
			isEditMode: false,
			shouldRefresh: false,
			selectedPerson: null,
			modalHeader: null,
		}
	},
	components: {
		Modal,
	},
	methods: {
		refreshPersons() {
			PersonDataService.retrieveAllPersons()
				.then(response => {
					this.Persons = response.data;
				})
				.catch(e => {
					console.log(e);
				});
		},
		addPerson() {
			this.modalHeader = 'Add Person';
			this.showModal = true;
			this.isEditMode = false;
		},
		updatePerson(id) {
			this.$router.push(`/Person/${id}`);
		},
		deletePerson(id) {
			if (confirm('Do you want to delete person?')) {
				PersonDataService.deletePerson(id)
					.then(() => {
						this.refreshPersons();
					});
			}
		},
		editPerson(Person) {
			this.isEditMode = true;
			this.modalHeader = 'Edit Person';
			this.selectedPerson = Person;
			this.showModal = true;
		},
		handleModalClose() {
			this.showModal = false;
			this.selectedPerson = null;
			this.shouldRefresh = true;
		},
	},
	created() {
		this.refreshPersons();
	},
	watch: {
		shouldRefresh(newVal) {
			if (newVal) {
				this.refreshPersons();
				this.shouldRefresh = false;
			}
		},
	},
};
</script>