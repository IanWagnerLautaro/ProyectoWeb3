const registrate = document.querySelector("#registrate-btn");
const login = document.querySelector("#login-btn");
const container = document.querySelector(".container");

registrate.addEventListener('click', () => {
    container.classList.add("sign-up-mode")
});

login.addEventListener('click', () => {
    container.classList.remove("sign-up-mode")
});