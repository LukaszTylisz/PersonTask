<template>
	<Transition name="modal">
		<div v-if="show" class="modal-mask">
			<div class="modal-container">
				<div class="modal-header">
					<slot name="header"></slot>
				</div>

				<div class="modal-body text-white">
					<form>
						<div class="form-group">
							<label for="firstName">FirstName: </label>
							<input type="text" class="form-control m-1" id="firstName" v-model="Person.firstName" />
							<small class="mx-3 mb-4 d-block">
								<span v-for="error of v$.Person.firstName.$errors" :key="error.$uid" class="validation-error-text">
									<strong>{{ error.$message }}</strong>
								</span>
							</small>
						</div>
						<div class="form-group">
							<label for="lastName">LastName:</label>
							<input type="text" class="form-control m-1" id="lastName" v-model="Person.lastName" />
							<small class="mx-3 mb-4 d-block">
								<span v-for="error of v$.Person.lastName.$errors" :key="error.$uid" class="validation-error-text">
									<strong>{{ error.$message }}</strong>
								</span>
							</small>
						</div>
						<div class="form-group">
							<div class="form-group">
								<label for="description">Description:</label>
								<input type="text" class="form-control m-1" id="description" v-model="Person.description" />
								<small class="mx-3 mb-4 d-block">
									<span v-for="error of v$.Person.description.$errors" :key="error.$uid" class="validation-error-text">
										<strong>{{ error.$message }}</strong>
									</span>
								</small>
							</div>
							<div class="form-group">
								<label for="emails">Emails:</label>
								<input type="text" class="form-control m-1" id="emails" v-model="Person.emails" />
								<small class="mx-3 mb-4 d-block">
									<span v-for="error of v$.Person.emails.$errors" :key="error.$uid" class="validation-error-text">
										<strong>{{ error.$message }}</strong>
									</span>
								</small>
							</div>
						</div>
					</form>
				</div>
				<div class="modal-footer">
					<slot name="footer">
						<div class="btn-group" role="group" aria-label="Basic example">
							<button
								v-if="edit"
								class="btn btn-success modal-default-button px-4 text-white"
								@click="updatePerson"
							>
								Save
							</button>
							<button v-else class="btn btn-success modal-default-button px-4 text-white" @click="addPerson">
								Add
							</button>
							<button class="btn btn-info modal-default-button px-4 text-white" @click="$emit('close'), clearForm()">
								Back
							</button>
						</div>
					</slot>
				</div>
			</div>
		</div>
	</Transition>
</template>

<script>
import PersonDataService from '../service/PersonDataService'
import { useVuelidate } from '@vuelidate/core'
import { required, maxLength, helpers } from '@vuelidate/validators'

export default {
	props: {
		show: Boolean,
		selected: Object,
		edit: Boolean
	},
	setup() {
		return { v$: useVuelidate() }
	},
	data() {
		return {
			Person: {
				id: '',
				firstName: '',
				lastName: '',
				description: '',
				emails: '',
			},
		}
	},
	validations() {
		return {
			Person: {
				firstName: {
					required: helpers.withMessage('This field can not be empty', required),
					maxLength: helpers.withMessage('Up 50 chars', maxLength(50)),
				},
				lastName: {
					required: helpers.withMessage('This field can not be empty', required),
					maxLength: helpers.withMessage('Up 50 chars', maxLength(50)),
				},
				description: {
				},
				emails: {
					required: helpers.withMessage('This field can not be empty', required),
				},
			},
		}
	},
	methods: {
		async addPerson() {
			const isFormCorrect = await this.v$.$validate()
			if (!isFormCorrect) {
				return
			}

			PersonDataService.createPerson(this.Person).then(() => {
				this.clearForm()
				this.$emit('close')
			}).catch(error => {
				alert(`Error - Check logs.\n\n Error code ${error}`)
			})
		},
		async updatePerson() {
			const isFormCorrect = await this.v$.$validate()
			if (!isFormCorrect) {
				return
			}

			PersonDataService.updatePerson(this.Person).then(() => {
				this.clearForm()
				this.$emit('close')
			}).catch(error => {
				alert(`Error - Check logs.\n\n Error code ${error}`)
			})
		},
		clearForm() {
			this.Person = {
				id: '',
				firstName: '',
				lastName: '',
				description: '',
				emails: '',
			}
		},
	},
	watch: {
		selected(newVal) {
			if (newVal) {
				this.Person = {
					id: this.selected.id,
					firstName: this.selected.firstName,
					lastName: this.selected.lastName,
					description: this.selected.description,
					emails: this.selected.emails,
				}
			}
		},
	},
}
</script>

<style></style>