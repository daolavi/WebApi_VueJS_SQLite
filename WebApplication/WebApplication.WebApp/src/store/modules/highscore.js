import _ from 'lodash';
import api from "@/utils/backend-api";
import { HighScore } from "@/models";
import {
  sendSuccessNotice,
  sendErrorNotice,
  closeNotice,
} from "@/utils/store-util.js";

const state = {
  highscores: [],
  loading: true,
  mode: "",
  snackbar: false,
  notice: "",
  highscore: new HighScore()
};

const getters = {};

const actions = {
  getHighScoreById({ commit }, id) {
    commit("setLoading", { loading: true });
    if (id) {
      api.getData("highscore/" + id).then(res => {
        const highscore = res.data;
        commit("setHighScore", { highscore });
        commit("setLoading", { loading: false });
      },
        err => {
          console.log(err);
        }
      );
    } else {
      commit("setHighScore", { highscore: new HighScore() });
      commit("setLoading", { loading: false });
    }
  },
  getAllHighScores({ commit }) {
    commit("setLoading", { loading: true });
    api.getData("highscore").then(res => {
      const highscores = res.data;
      commit("setHighScores", highscores);
      commit("setLoading", { loading: false });
    });
  },
  deleteHighScore({ commit, dispatch }, id) {
    api.deleteData("highscore/" + id.toString()).then(res => {
      return new Promise((resolve, reject) => {
        const highscore = res.data;
        commit("deleteHighScore", { highscore });
        sendSuccessNotice(commit, "Operation is done.");
        resolve();
      });
    })
      .catch(err => {
        console.log(err);
        sendErrorNotice(commit, "Operation failed! Please try again later. ");
        closeNotice(commit, 1500);
      });
  },
  saveHighScore({ commit, dispatch }, highscore) {
    if (!highscore.id) {
      api.postData("highscore", highscore).then(res => {
        const highscore = res.data;
        commit("setHighScore", { highscore });
        sendSuccessNotice(commit, "New HighScore has been added.");
      })
        .catch(err => {
          console.log(err);
          sendErrorNotice(commit, "Operation failed! Please try again later. ");
          closeNotice(commit, 1500);
        });
    } else {
      api.putData("highscore", highscore).then(res => {
        const highscore = res.data;
        commit("setHighScore", { highscore });
        sendSuccessNotice(commit, "HighScore has been updated.");
      })
        .catch(err => {
          console.log(err);
          sendErrorNotice(commit, "Operation failed! Please try again later. ");
          closeNotice(commit, 1500);
        });
    }
  },
  closeSnackBar({ commit }, timeout) {
    closeNotice(commit, timeout);
  }
};

const mutations = {
  setHighScores(state, highscores) {
    state.highscores = highscores;
  },
  setLoading(state, { loading }) {
    state.loading = loading;
  },
  setNotice(state, { notice }) {
    state.notice = notice;
  },
  setSnackbar(state, { snackbar }) {
    state.snackbar = snackbar;
  },
  setMode(state, { mode }) {
    state.mode = mode;
  },
  setHighScore(state, { highscore }) {
    state.highscore = highscore;
  },
  deleteHighScore(state, { highscore }) {
    state.highscores = _.filter(state.highscores, function(item){
      return item.id !== highscore.id;
    });
  },
};

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters,
};
