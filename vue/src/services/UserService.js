import axios from 'axios';

export default {

  getUser(id) {
    return axios.get(`/users/${id}`)
  },

  saveToExcluded(id, movieId) {
    return axios.post(`users/${id}/exclude`, movieId)
  }

}
