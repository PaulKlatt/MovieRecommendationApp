import axios from 'axios';

export default {

  getGenres() {
    return axios.get('/movies/genrelist')
  },

  getRandomMovie(queryEnd){
    return axios.get(`/movies/genres/${queryEnd}`);
  },

  saveToFavorites(userId, movie){
    return axios.post(`/movies/users/${userId}`, movie);
  },

  findMoviesByUserId(userId){
    return axios.get(`/movies/users/${userId}`);
  },

  banMovie(movieId, userId){
    return axios.post(`/movies/${movieId}/users/${userId}/ban`);
  }
}
