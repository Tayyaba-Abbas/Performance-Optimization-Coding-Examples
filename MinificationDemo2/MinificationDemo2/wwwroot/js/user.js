function submitUserLoginForm() {
    var userName = document.getElementById('txtUserName');
    var password = document.getElementById('txtPassword');
    if (validateUserName(userName) && validatePassword(password)) {
        alert('Validateion passed and from submitted ');
    }
    else {
        alert('please correct username and password');
    }
}