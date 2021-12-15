import axios from 'axios';

export default {

  getUser(id) {
    return axios.get(`/users/${id}`)
  },

  updatePassword(user){
    return axios.put('/users/update', user)
  },
  
  saveToExcluded(id, movieId) {
    return axios.post(`users/${id}/exclude`, movieId)
  },
  deleteUser(id){
    return axios.delete(`/users/${id}`)
  }

}
