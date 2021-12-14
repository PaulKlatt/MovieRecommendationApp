import axios from 'axios';

export default {

  getUser(id) {
    return axios.get(`/users/${id}`)
  },

  saveToExcluded(id, movieInfo) {
    return axios.post(`users/${id}/exclude`, movieInfo)
  }

}
