
$(() => {

    function Login(dto) {

        if (dto.Email == "" || dto.Password == "") {
            toastr.error("Lütfen boş alanları doldurunuz.", 'Uyarı', {
                positionClass: 'toast-bottom-left'
            });
            return;
        }

        var opt = {
            url: "/Login/SignIn",
            data: {
                Email: dto.Email,
                Password: dto.Password
            },
            event: function (rsp) {
                if (rsp.ResultStatus == 0) {
                    console.log(rsp);
                    localStorage.setItem("guid", rsp.Data)
                    window.location.replace("/Home");
                }
                else {
                    toastr.error(rsp.ErrorMessage, 'Uyarı', {
                        positionClass: 'toast-bottom-left'
                    });
                }
            }
        }

        SendRequest(opt);
    }

    function Register(dto) {

        if (dto.TCKN == "" || dto.Name == "" || dto.Surname == "" || dto.Email == "" || dto.PhoneNumber == "" || dto.Password == "") {
            toastr.error("Lütfen boş alanları doldurunuz.", 'Uyarı', {
                positionClass: 'toast-bottom-left'
            });
            return;
        }

        var opt = {
            url: "/Login/Register",
            data: {
                TCKN: dto.TCKN,
                Name: dto.Name,
                Surname: dto.Surname,
                Email: dto.Email,
                PhoneNumber: dto.PhoneNumber,
                Password: dto.Password
            },
            event: function (rsp) {
                if (rsp.ResultStatus == 0) {
                    console.log(rsp);
                    localStorage.setItem("guid", rsp.Data)
                    window.location.replace("/Home");
                }
                else {
                    toastr.error(rsp.ErrorMessage, 'Uyarı', {
                        positionClass: 'toast-bottom-left'
                    });
                }
                console.log(rsp);
            }
        }

        SendRequest(opt);
    }

    function ResetPassword(dto) {

        if (dto.Email == "") {
            toastr.error("Lütfen mail adresinizi giriniz.", 'Uyarı', {
                positionClass: 'toast-bottom-left'
            });
            return;
        }

        var opt = {
            url: "/Login/ResetPassword",
            data: {
                Email: dto.Email
            },
            event: function (rsp) {
                if (rsp.ResultStatus == 0) {

                    console.log(rsp);

                    $("#txtForgetEmail").val("");

                    toastr.success(rsp.SuccessMessage, 'Bilgilendirme', {
                        positionClass: 'toast-bottom-left'
                    });
                }
                else {
                    toastr.error(rsp.ErrorMessage, 'Uyarı', {
                        positionClass: 'toast-bottom-left'
                    });
                }
            }
        }

        SendRequest(opt);
    }


    $(document).on("click", ".btnLogin", function (e) {
        e.preventDefault();

        var frmLogin = document.frmLogin;
        let form = new FormData(frmLogin);

        dto = {
            Email: form.get("email"),
            Password: form.get("password")
        }

        console.log(dto);

        Login(dto);
    });

    $(document).on("click", ".btnSignUp", function (e) {
        e.preventDefault();

        var frmRegister = document.frmRegister;
        let form = new FormData(frmRegister);

        dto = {
            TCKN: form.get("txtTckn"),
            Name: form.get("txtName"),
            Surname: form.get("txtSurname"),
            Email: form.get("txtEmail"),
            PhoneNumber: form.get("txtPhoneNumber"),
            Password: form.get("txtPassword"),
        }

        console.log(dto);

        Register(dto);
    });


    $(document).on("click", ".btnForgetPassword", function (e) {
        e.preventDefault();

        var frmForgetPassword = document.frmForgetPassword;
        let form = new FormData(frmForgetPassword);

        dto = {
            Email: form.get("txtForgetEmail"),
        }

        ResetPassword(dto);
    });

});





