<template>
	<Transition name="modal">
		<div v-if="show" class="modal-mask">
			<div class="modal-container">
				<div class="modal-header">
					<slot name="header"></slot>
				</div>

				<div class="modal-body text-white">
					<form @submit.prevent="savePerson">
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
							<label for="description">Description:</label>
							<input type="text" class="form-control m-1" id="description" v-model="Person.description" />
							<small class="mx-3 mb-4 d-block">
								<span v-for="error of v$.Person.description.$errors" :key="error.$uid" class="validation-error-text">
									<strong>{{ error.$message }}</strong>
								</span>
							</small>
						</div>
						<div v-if="!edit">
							<div class="form-group">
								<label for="emails">Emails:</label>
								<div v-for="(email, index) in Person.emails" :key="index" class="d-flex">
									<input type="text" class="form-control m-1" v-model="email.email" />
									<button type="button" class="btn btn-danger m-1" @click="removeEmail(index)">Remove</button>
								</div>
								<button type="button" class="btn btn-secondary m-1" @click="addEmail">Add Email</button>
								<small class="mx-3 mb-4 d-block">
									<span v-for="error of v$.Person.emails.$errors" :key="error.$uid" class="validation-error-text">
										<strong>{{ error.$message }}</strong>
									</span>
								</small>
							</div>
						</div>
						<div v-else>
							<div class="form-group">
								<label for="emails">Emails:</label>
								<div v-for="(email, index) in Person.emails" :key="index" class="d-flex align-items-center">
									<input type="text" class="form-control m-1" v-model="email.emailAddress" />
									<button type="button" class="btn btn-danger m-1" @click="removeEmail(index)">Remove</button>
								</div>
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
							<button v-if="!edit" class="btn btn-success modal-default-button px-4 text-white" @click="savePerson">
								Add
							</button>
							<button v-else class="btn btn-success modal-default-button px-4 text-white" @click="savePerson">
								Save
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
				emails: []
			}
		}
	},
	validations() {
		return {
			Person: {
				firstName: {
					required: helpers.withMessage('This field cannot be empty', required),
					maxLength: helpers.withMessage('Up to 50 characters', maxLength(50)),
				},
				lastName: {
					required: helpers.withMessage('This field cannot be empty', required),
					maxLength: helpers.withMessage('Up to 50 characters', maxLength(50)),
				},
				description: {},
				emails: {
					required: helpers.withMessage('Please enter at least one email address', required),
					$each: {
						emailAddress: {
							required: helpers.withMessage('Email address cannot be empty', required)
						}
					}
				},
			},
		}
	},
	methods: {
		async savePerson() {
			const isFormCorrect = await this.v$.$validate()
			if (!isFormCorrect) {
				return
			}
			
			if (this.edit) {
				this.updatePerson()
			} else {
				this.addPerson()
			}
		},
		async addPerson() {
			try {
				await PersonDataService.createPerson(this.Person)
				this.clearForm()
				this.$emit('close')
			} catch (error) {
				alert(`Error - Check logs.\n\n Error code ${error}`)
			}
		},
		async updatePerson() {
			try {
				await PersonDataService.updatePerson(this.Person)
				this.clearForm()
				this.$emit('close')
			} catch (error) {
				alert(`Error - Check logs.\n\n Error code ${error}`)
			}
		},
		clearForm() {
			this.Person = {
				id: '',
				firstName: '',
				lastName: '',
				description: '',
				emails: []
			}
		},
		addEmail() {
			this.Person.emails.push({ emailAddress: '' })
		},
		removeEmail(index) {
			this.Person.emails.splice(index, 1)
		},
	},
	watch: {
		selected(newVal) {
			if (newVal) {
				this.Person = {
					id: newVal.id,
					firstName: newVal.firstName,
					lastName: newVal.lastName,
					description: newVal.description,
					emails: newVal.emails || []
				}
			}
		},
	},
}
</script>

<style>
.validation-error-text {
	color: red;
}
</style>
