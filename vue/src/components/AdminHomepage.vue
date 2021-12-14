<template>
  <div id="admin-homepage">
      <button id='addMovies'>add movies</button>
      <button id='modifyAccount'>modify accounts</button>
  </div>
</template>

<script>
export default {

}
</script>

<style>

#addMovies, #modifyAccount {
  background-color: #f67280; font-size: larger; 
      color: #355c7d; border: 1px solid #266DB6; box-sizing: border-box; font-weight: 700;
      line-height: 24px; padding: 16px 23px; position: relative; text-decoration: none;  
      box-shadow: 3px 3px #f8b195;
      padding: 0.25em 0.5em;
      user-select: none; touch-action: manipulation;
      cursor: pointer;
}

#addMovies:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}

#modifyAccount:active {
  box-shadow: 0px 0px 0px 0px;
  top: 5px;
  left: 5px;
}


</style>