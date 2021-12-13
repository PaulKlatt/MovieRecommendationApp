import axios from 'axios';

export default {

  getUser(id) {
    return axios.get(`/users/${id}`)
  },

<<<<<<< HEAD
  updatePassword(user){
    return axios.put('/update', user)
=======
  saveToExcluded(id, movieId) {
    return axios.post(`users/${id}/exclude`, movieId)
>>>>>>> ea17b88ef4f948123b53fdfca9b607fd52910a61
  }

}
