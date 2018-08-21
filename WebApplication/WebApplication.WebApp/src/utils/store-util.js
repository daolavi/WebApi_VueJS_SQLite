export function sendSuccessNotice (commit, notice) {
  commit("setNotice", {
    notice,
  });
  commit("setSnackbar", {
    snackbar: true,
  });
  commit("setMode", {
    mode: "success",
  });
}

export function sendErrorNotice (commit, notice) {
  commit("setNotice", {
    notice,
  });
  commit("setSnackbar", {
    snackbar: true,
  });
  commit("setMode", {
    mode: "error",
  });
}

export function closeNotice (commit, timeout) {
    setTimeout(() => {
      commit("setSnackbar", {
        snackbar: false,
      });
      commit("setNotice", {
        notice: "",
      });
      commit("setMode", {
        mode: "",
      });
    }, timeout);
}
