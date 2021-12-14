<template>
  <div class='homepage'>
    <div class="description">
      <h1 class="title">The Superior Movie Recommendation App</h1>
      <p>Have you ever been at home, staring at the netflix screen, unsure of what to watch? Well worry no more!  The Superior Movie Recommendation App has got you covered.</p>
    </div>
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h2 class="h3 mb-3 font-weight-normal">Please Sign In</h2>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
      <div>
        <label for="username" class="sr-only">Username</label>
        <input
          type="text"
          id="username"
          class="form-control"
          placeholder="Username"
          v-model="user.username"
          required
          autofocus
        />
      </div>
      <div>
        <label for="password" class="sr-only">Password</label>
        <input
          type="password"
          id="password"
          class="form-control"
          placeholder="Password"
          v-model="user.password"
          required
        />
      </div>
      <div>
      <router-link :to="{ name: 'register' }">Need an account?</router-link>
      </div>
      <div>
      <button type="submit">Sign in</button>
      </div>
    </form>
    <div id="app-details">
      <p>Browse random movies from a large, varied collection by your favorite genres! Save your favorite movies for later reference!</p>
    </div>    
  </div>
  <img class = "bestcat" src="https://canary.contestimg.wish.com/api/webimage/5e1468224b7eec03c97f8915-large.jpg?cache_buster=085e00180f70ca62aa0f8a1c7d8e6bdb" />
  </div>
</template>

<script>
import authService from "../services/AuthService";
import movieService from "../services/MovieService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
      movieService
        .getGenres()
        .then( response => {
          if (response.status == 200){
            this.$store.commit('SET_GENRES', response.data.genres);
            this.$router.push("/");
          }
        })
        .catch ( error => {
        //ERROR HANDLING
        console.log('Something messed up' + error)
        });
      
    }
  }
};
</script>

<style scoped>

.description {
  text-align: center;
  grid-area: description;
}

#login {
  grid-area: login;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.form-signin * {
  margin: 10px 5px;
}

#app-details {
  /* try to center this with login info? */
  width:50%;
  flex-wrap: wrap;
  text-align: center;
}

.homepage {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas: 
  "description description"
   "login cat";
}

.bestcat {
  grid-area: cat;
}

@media only screen and (max-width: 768px) {
.homepage {
    display:grid;
    grid-template-columns: 1fr;
    grid-template-areas: 
    "description"
    "login"
    "cat";

  }
  .bestcat{
    display: block;
    margin-left: auto;
    margin-right: auto;
    width: 95%;

  }
  .form-signin * {
   display: block;
    margin-left: auto;
    margin-right: auto;
    width: 95%;
}

h2{
    display: block;
    margin-left: auto;
    margin-right: auto;
    width: 95%;
    font-size: 1.25em;
}

p{
      display: block;
    margin-left: auto;
    margin-right: auto;
    width: 95%;
    font-size:1em;

}
#app-details {
  /* try to center this with login info? */
  width:95%;
  flex-wrap: wrap;
  text-align: center;
  margin-top: 16px;
  margin-bottom: 16px;
}
}
</style>