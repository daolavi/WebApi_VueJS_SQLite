<template>
  <v-container fluid>
    <v-flex xs12>
      <v-card class="grey lighten-4 elevation-0">
        <v-card-title style="font-size:20px!important;font-weight:500">
          {{title}}
          <v-spacer></v-spacer>
          <v-btn fab small class="grey" @click.native="cancel()">
            <v-icon>cancel</v-icon>
          </v-btn>
          &nbsp;
          <v-btn fab small class="purple" :disabled="!valid" @click.native="save()">
            <v-icon>save</v-icon>
          </v-btn>
        </v-card-title>
        <v-card-text v-if="loading !== true" >
          <v-container fluid grid-list-sm>
            <v-form ref="form" v-model="valid" lazy-validation>
              <v-flex md3 sm6 xs12 class="mx-1 px-0">
                <v-text-field label="Name" maxlength="1000" hint="Name is required" value="Input text" v-model="highscore.name" class="input-group--focused" :rules="nameRule" required></v-text-field>
              </v-flex>
              <v-flex md3 sm6  xs12  class="mx-1 px-0">
                <v-text-field type="number" label="Score" hint="Score is required" v-model="highscore.score" class="input-group--focused" :rules="scoreRule" required></v-text-field>
              </v-flex> 
            </v-form>
          </v-container>
        </v-card-text>
      </v-card>
    </v-flex>
    <v-snackbar v-if="loading===false" :right="true" :timeout="timeout" :color="mode" v-model="snackbar" >
      {{ notice }}
      <v-btn dark flat @click.native="closeSnackbar">Close</v-btn>
    </v-snackbar>
  </v-container>
</template>
<script>
import { mapState, dispatch } from "vuex";
export default {
  data() {
    return {
      title: "",
      valid: true,
      nameRule: [
        name => !!name || 'Name is required',
        name => (name && name.length <= 100) || 'Name must be less than 100 characters'
      ],
      scoreRule: [
        score => !!score || 'Score is required',
        score => (score >= 0 || score <= 999999) || 'Score must be between 0 and 999999'
      ],
      timeout: 2000,
    };
  },
  computed: {
    ...mapState("highscore", {
      highscore: "highscore",
      loading: "loading",
      mode: "mode",
      snackbar: "snackbar",
      notice: "notice"
    })
  },
  methods: {
    save() {
      if (this.$refs.form.validate()) {
        const highscore = { ...this.highscore };
        highscore.score = parseInt(highscore.score);
        console.log(highscore);
        Store.dispatch("highscore/saveHighScore", highscore).then(() => {
          Store.dispatch("highscore/closeSnackBar", 2000);
        });
      }
    },
    cancel() {
      this.$router.push({ name: "HighScores" });
    },
    closeSnackbar() {
      Store.commit("highscore/setSnackbar", { snackbar: false });
      Store.commit("highscore/setNotice", { notice: "" });
    }
  },
  created() {
    Store.dispatch("highscore/getHighScoreById", this.$route.params.id);
  },
  mounted() {
    this.title = this.$route.params.id ? "Edit HighScore" : "New HighScore";
  }
};
</script>
