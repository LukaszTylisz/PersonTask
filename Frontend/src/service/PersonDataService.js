import axios from 'axios'

const PERSON_API_URL = 'https://localhost:7000/api/Person'

class PersonDataService {

    retrieveAllPersons() {
        return axios.get(`${PERSON_API_URL}/`);
    }

    retrievePerson(id) {
        return axios.get(`${PERSON_API_URL}/${id}`);
    }

    deletePerson(id) {
        return axios.delete(`${PERSON_API_URL}/${id}`);
    }
    
    updatePerson(person) {
        return axios.put(`${PERSON_API_URL}/`, person);
    }

    createPerson(person) {
        return axios.post(`${PERSON_API_URL}/`, person);
    }
  }
export default new PersonDataService()