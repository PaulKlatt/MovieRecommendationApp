import axios from 'axios';

export default {

  getUser(id) {
    return axios.get(`/users/${id}`)
  },

  saveToExcluded(id, movieInfo) {
    return axios.post(`users/${id}/exclude`, movieInfo)
  },
  deleteUser(id){
    return axios.delete(`/users/${id}`)
  },
  updatePassword(user){
    return axios.put('/users/update', user);
  }
}
