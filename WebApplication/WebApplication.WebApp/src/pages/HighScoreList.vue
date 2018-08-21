<template>
  <v-container fluid>
    <v-flex xs12>
      <p class="text-xs-center display-2">High Scores</p>
       <v-card>
          <v-card-title>
            <span class="title">High Scores ({{highscores.length}})</span>
            <v-spacer></v-spacer> 
            <v-btn class="green darken-3" fab small dark @click.native="addHighScore">
              <v-icon>add</v-icon>
            </v-btn>
        </v-card-title>
        <Table v-if="loading===false" :headers="headers" :items="highscores" @edit="editHighScore" @delete="deleteHighScore"></Table>
      </v-card>
    </v-flex>
    <confirm-dialog :dialog="dialog" :dialogTitle="dialogTitle" :dialogText="dialogText" @onConfirm="onConfirm" @onCancel="onCancel"></confirm-dialog>
    <v-snackbar v-if="loading===false" :right="true" :timeout="timeout" :color="mode" v-model="snackbar" >
      {{ notice }}
      <v-btn dark flat @click.native="closeSnackbar">Close</v-btn>
    </v-snackbar>
  </v-container>
</template>
<script>
import Table from "@/components/Table.vue";
import ConfirmDialog from "@/components/ConfirmDialog.vue";
import { mapState } from "vuex";

export default {
  components: {
    Table,
    ConfirmDialog
  },
  data() {
    return {
      dialog: false,
      dialogTitle: "High Score Delete Dialog",
      dialogText: "Do you want to delete this high score?",
      right: true,
      headers: [
        { text: "Name", value: "name" },
        { text: "Score", value: "score" }
      ],
      highScoreId: null,
      query: "",
      snackbarStatus: false,
      timeout: 2000,
      color: ""
    };
  },
  methods: {
    print() {
      window.print();
    },
    editHighScore(item) {
      this.$router.push({ name: "HighScore", params: { id: item.id } });
    },
    addHighScore() {
      this.$router.push("newhighscore");
    },
    deleteHighScore(item) {
      this.highScoreId = item.id;
      this.dialog = true;
    },
    onConfirm() {
      Store.dispatch("highscore/deleteHighScore", this.highScoreId).then(() => {
        Store.dispatch("highscore/closeSnackBar", 2000);
      });
      this.dialog = false;
    },
    onCancel() {
      this.dialog = false;
    },
    closeSnackbar() {
      Store.commit("highscore/setSnackbar", { snackbar: false });
      Store.commit("highscore/setNotice", { notice: "" });
    }
  },
  computed: {
    ...mapState("highscore", {
      highscores: "highscores",
      loading: "loading",
      mode: "mode",
      snackbar: "snackbar",
      notice: "notice"
    })
  },
  created() {
    Store.dispatch("highscore/getAllHighScores");
  },
  mounted() {}
};
</script>
