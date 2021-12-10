import axios from 'axios';

export default {

  getGenres() {
    return axios.get('https://api.themoviedb.org/3/genre/movie/list?api_key=bb16218327fa750dbdfc80a7ff286caf&language=en-US')
  },

  getMovies(genreIds){
      return axios.get(`https://api.themoviedb.org/3/discover/movie?api_key=bb16218327fa750dbdfc80a7ff286caf&language=en-US&include_adult=false&include_video=false&with_genres=${genreIds}`)
  }
}
