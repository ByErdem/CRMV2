$(() => {

    $(document).on('click', '#filter_search', function () {
        $('#filter_inputs').slideToggle("slow");
    });

    $(document).on("click", ".btnSaveUser", function (e) {
        e.preventDefault();
        var frmAddUser = document.frmAddUser;
        let form = new FormData(frmAddUser);

        dto = {
            txtTckn: form.get("txtTckn"),
            txtEmail: form.get("txtEmail"),
            txtName: form.get("txtName"),
            txtSurname: form.get("txtSurname"),
            txtPhoneNumber: form.get("txtPhoneNumber"),
            txtPassword: form.get("txtPassword"),
        }
    });

    if ($('.select').length > 0) {
        $('.select').select2({
            minimumResultsForSearch: -1,
            width: '100%'
        });
    }

    var selectAllItems = "#select-all";
    var checkboxItem = ":checkbox";
    $(selectAllItems).click(function () {

        if (this.checked) {
            $(checkboxItem).each(function () {
                this.checked = true;
            });
        } else {
            $(checkboxItem).each(function () {
                this.checked = false;
            });
        }

    });

    $(document).on('click', '#btnEdit', function (e) {
        e.preventDefault();
        var tr = $(this).closest('tr');
        var rowData = tr.data('rowData');
        createModalFormWithRowData(".dataTable", rowData, "Kullanıcı Düzenle", tr, "update", function (e) {
            console.log(e);
        });
    });

    $(document).on("click", ".btnAddUser", function (e) {
        e.stopPropagation();
        //var table = $(".dataTable tbody tr[1]");
        //console.log(table);
        //var tr = $(this).closest('tr');
        //var rowData = tr.data('rowData');
        createModalFormWithRowData(".dataTable", {}, "Yeni Kullanıcı Ekle", undefined, "new", function (d) {

            console.log(d);

            var data = {
                TCKN: d.UserDetail.TCKN,
                Email: d.Email,
                PasswordHash: d.PasswordHash,
                PhoneNumber: d.PhoneNumber,
                DateOfBirth: d.DateOfBirth,
                Name: d.UserDetail.Name,
                Surname: d.UserDetail.Surname,
                RoleName: d["UserRoles[0]"]["Role"]["Name"]
            }

            var opt = {
                url: "/Admin/AddUser",
                data: data,
                event: function (rsp) {
                    if (rsp.ResultStatus == 0) {
                        LoadDataTable();
                        toastr.success(rsp.SuccessMessage, 'Bilgilendirme', {
                            positionClass: 'toast-bottom-right'
                        });
                    }
                    else {
                        toastr.error(rsp.ErrorMessage, 'Uyarı', {
                            positionClass: 'toast-bottom-right'
                        });
                    }
                }
            }

            SendRequest(opt);
        });
    });


    //$(document).on('click', '#btnEdit', function (e) {
    //e.preventDefault();

    //});

    $(document).on('click', '#btnDelete', function (e) {
        e.preventDefault();
        var tr = $(this).closest('tr');
        var rowData = tr.data('rowData');
        console.log(rowData);

        var data = {
            Id: rowData.Id
        }

        console.log(data);

        var opt = {
            url: "/Admin/DeleteUserById",
            data: data,
            event: function (rsp) {
                if (rsp.ResultStatus == 0) {
                    LoadDataTable();
                    toastr.success(rsp.SuccessMessage, 'Bilgilendirme', {
                        positionClass: 'toast-bottom-right'
                    });
                }
                else {
                    toastr.error(rsp.ErrorMessage, 'Uyarı', {
                        positionClass: 'toast-bottom-right'
                    });
                }
            }
        }

        SendRequest(opt);

    });

    $(document).on("keyup", ".txtSearchBar", function () {

        var value = $(this).val().toLowerCase();

        if (!value) {
            $(".dataTable tr").show();
            return;
        }

        $(".dataTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });

    });


    function LoadDataTable() {
        var opt = {
            url: "/Admin/GetAllUsersByNonDeleted",
            event: function (rsp) {
                if (rsp.ResultStatus == 0) {
                    console.log(rsp);

                    var columns = [
                        { "field": "UserDetail.TCKN", "header": "TC Kimlik No", editable: true, type: "input", required: true },
                        { "field": "UserDetail.Name", "header": "Adı", editable: true, type: "input", required: true },
                        { "field": "UserDetail.Surname", "header": "Soyadı", editable: true, type: "input", required: true },
                        { "field": "UserDetail.DateOfBirth", "header": "Doğum Tarihi", editable: true, type: "datetime", required: true },
                        { "field": "Email", "header": "E-Posta Adresi", editable: true, type: "email", required: true },
                        { "field": "PasswordHash", "header": "Şifre", editable: true, type: "password", required: true, visible:false, hideValue:true },
                        { "field": "PhoneNumber", "header": "Telefon Numarası", editable: true, type: "tel", required: true },
                        { "field": "UserRoles[0].Role.Name", "header": "Rolü", editable: true, type: "input", required: true },
                        {
                            "render": function () {
                                return `
                                <a class="action-set" href="javascript:void(0);" data-bs-toggle="dropdown" aria-expanded="true">
                                    <i class="fa fa-ellipsis-v" aria-hidden="true"></i>
                                </a>
									<ul class="dropdown-menu">
										<li>
											<a id="btnEdit" class="dropdown-item"><img src="/assets/img/icons/edit.svg" class="me-2" alt="img">Düzenle</a>
										</li>
										<li>
											<a id="btnDelete" class="dropdown-item confirm-text"><img src="/assets/img/icons/delete1.svg" class="me-2" alt="img">Sil</a>
										</li>
									</ul>
                                `;
                            },
                            "header": "İşlem"
                        }
                    ];


                    addDataToTable(".dataTable", rsp.Data, columns);
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




    LoadDataTable();

});
