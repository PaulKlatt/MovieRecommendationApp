<template>
  <div class='homepage'>
    <div class="description">
      <h1 class="title">catflix.</h1>
      <p>have you ever been at home, staring at the netflix screen, unsure of what to watch? well worry no more! catflix has got you covered.</p>
    </div>
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h2 class="h3 mb-3 font-weight-normal">please sign in...</h2>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >invalid username and password!</div>
      <div
        id='successAlert'
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >thank you for registering, please sign in</div>
      <div>
        <label for="username" class="sr-only">username:</label>
        <input
          type="text"
          id="username"
          class="form-control"
          placeholder="username"
          v-model="user.username"
          required
          autofocus
        />
      </div>
      <div>
        <label for="password" class="sr-only">password:</label>
        <input
          type="password"
          id="password"
          class="form-control"
          placeholder="password"
          v-model="user.password"
          required
        />
      </div>
      <div>
      <router-link id="toRegister" :to="{ name: 'register' }">need an account? </router-link> 
      </div>
      <div>
      <button id='buttonSubmit' type="submit" 

      >sign in</button>

      </div>
    </form>
    <div id="app-details">
      <p>browse random movies from a large, varied collection by your favorite genres! save your favorite movies for later reference!</p>
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

#successAlert {
  color: #c06c84;
}
#toRegister{
  color: #f8b195;
}

#buttonSubmit {
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
}

#buttonSubmit:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

.form-signin * {
  margin: 10px 5px;
}

#toRegister {
  color: fushia;
}

#app-details {
  /* try to center this with login info? */
  width: 50%;
  flex-wrap: wrap;
  text-align: center;
}

.homepage {
  display: grid;
  grid-template-columns: 1.25fr 1fr;
  grid-template-areas: 
  "description description"
   "login cat";
}

.bestcat {
  grid-area: cat;
  width: 400px;
  height: 400px;
  object-fit: cover;
  object-position: 50%;
  border: 10px solid #6c5b7b
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

  @media (min-width: 768px) {
    .form-signin-submit-button {
      padding: 16px 32px;
    }
  }
}


</style>

