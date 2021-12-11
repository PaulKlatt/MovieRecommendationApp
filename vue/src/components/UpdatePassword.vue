<template>
    <div class='updatePassword'>
    <h3>Reset your password:</h3>
    <label for="currentPassword">Current Password</label>
    <form class='updatePassword'>
    <input
                     type="password"
                     id="currentPassword"
                     class="form-control"
                     placeholder="Current Password"
    />
                    <label for="password">New Password</label>

                    <input
                      type="password"
                      id="password"
                      class="form-control"
                      placeholder="Password"
    />
    <label for="password">Confirm Password</label>
                      <input
                      type="password"
                      id="confirmPassword"
                      class="form-control"
                      placeholder="Confirm Password"
                      required
                    />
                     <button class="btn btn-lg btn-primary btn-block" type="submit">
                      Update Password
                      </button>
    </form>
    </div>
</template>