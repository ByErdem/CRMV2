function SendRequest(opt) {

    var token = localStorage.getItem("guid");

    var settings = {
        url: opt.url,
        type: 'GET',
        contentType: "application/json",
        success: function (rsp) {
            opt.event(rsp);
        },
        error: function (req, status, error) {
            console.error("Error:", status, error);
            opt.event(null, error);
        }
    };

    // Token varsa, 'beforeSend' fonksiyonunu ayarlar
    if (opt.tokenNeeded != undefined) {
        if (opt.tokenNeeded) {
            if (token) {
                settings.beforeSend = function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token);
                };
            }
        }
    }

    if (opt.data) {
        settings.type = "POST";
        settings.data = JSON.stringify(opt.data);
    }

    $.ajax(settings);
}

function addDataToTable2(tableId, data, columns) {
    var table = $(tableId);

    // Store columns data in the table
    table.data('columns', columns);

    // Check and create thead and tbody if they don't exist
    var thead = table.find('thead');
    if (thead.length === 0) {
        thead = $('<thead>').appendTo(table);
    }
    var tbody = table.find('tbody');
    if (tbody.length === 0) {
        tbody = $('<tbody>').appendTo(table);
    }

    // Add headers
    thead.empty();
    var headerRow = "<tr>";
    columns.forEach(function (column) {
        if (column.visible === false) return; // Skip columns marked as not visible
        headerRow += "<th>" + column.header + "</th>";
    });
    headerRow += "</tr>";
    thead.append(headerRow);

    // Add data rows
    tbody.empty();
    data.forEach(function (item) {
        var dataRow = $("<tr>").data("rowData", item);
        columns.forEach(function (column) {
            if (column.visible === false) return; // Skip columns marked as not visible
            var value = column.render ? column.render(item) : getNestedValue(item, column.field);
            dataRow.append("<td>" + (value !== undefined ? value : "") + "</td>");
        });
        tbody.append(dataRow);
    });
}

function addDataToTable(tableId, data, columns) {

    //Öncelikle tabloyu sil ve yeniden oluştur.
    var $table = $("table").addClass("table").addClass(tableId);
    $(".table-responsive").html("");
    $(".table-responsive").append($table);

    var table = $(tableId);
    var tbody = table.find('tbody');

    // Eğer tablo verilerle doluysa temizle
    if (tbody.length > 0) {
        tbody.empty();
    }

    // Store columns data in the table
    table.data('columns', columns);

    // Eğer tablo verilerle dolu değilse, tabloyu oluştur
    if (tbody.length === 0) {
        var thead = table.find('thead');
        if (thead.length === 0) {
            thead = $('<thead>').appendTo(table);
        }

        tbody = $('<tbody>').appendTo(table);

        // Add headers
        thead.empty();
        var headerRow = "<tr>";
        columns.forEach(function (column) {
            if (column.visible === false) return; // Görünmez olarak işaretlenen sütunları atla
            headerRow += "<th>" + column.header + "</th>";
        });
        headerRow += "</tr>";
        thead.append(headerRow);
    }

    // Add data rows
    data.forEach(function (item) {
        var dataRow = $("<tr>").data("rowData", item);
        columns.forEach(function (column) {
            if (column.visible === false) return; // Görünmez olarak işaretlenen sütunları atla
            var value = column.render ? column.render(item) : getNestedValue(item, column.field);
            dataRow.append("<td>" + (value !== undefined ? value : "") + "</td>");
        });
        tbody.append(dataRow);
    });
}

function getNestedValue(obj, path) {
    if (!path) return ''; // Eğer path boş veya undefined ise boş string dön
    return path.split('.').reduce(function (prev, curr) {
        if (prev === null || prev === undefined) return null;
        if (curr.includes('[')) {
            // Dizi indekslerini işle
            var parts = curr.split('[');
            var arrayField = parts[0];
            var arrayIndex = parseInt(parts[1]);
            return (prev[arrayField] !== undefined) ? prev[arrayField][arrayIndex] : undefined;
        } else {
            return prev[curr];
        }
    }, obj || self);
}

function setNestedValue(jsonData, fieldName, newValue) {
    var fieldNames = fieldName.split('.'); // Alan adını nokta ile böler
    var currentData = jsonData;

    for (var i = 0; i < fieldNames.length - 1; i++) {
        if (!currentData[fieldNames[i]]) {
            currentData[fieldNames[i]] = {};
        }
        currentData = currentData[fieldNames[i]];
    }

    currentData[fieldNames[fieldNames.length - 1]] = newValue;
}

function createModalFormWithRowData(tableId, rowData, modalTitle, trElement, processType, event) {
    // Tablonun sütunlarını al
    var columns = $(tableId).data('columns');

    // Modal oluştur
    var modal = $('<div>').addClass('modal fade').attr({
        id: 'dynamicModal',
        tabindex: '-1',
        'aria-labelledby': 'dynamicModalLabel',
        'aria-hidden': 'true'
    }).append(
        $('<div>').addClass('modal-dialog modal-lg').append(
            $('<div>').addClass('modal-content').append(
                $('<div>').addClass('modal-header').append(
                    $('<h5>').addClass('modal-title').text(modalTitle),
                    $('<button>').addClass('btn-close').attr({
                        type: 'button',
                        'data-bs-dismiss': 'modal',
                        'aria-label': 'Close'
                    }).text("x")
                ),
                $('<form>').addClass('modal-body form').attr('name', 'dynamicModalForm'),
                $('<div>').addClass('modal-footer justify-content-end').append(
                    $('<button>').addClass('btn btn-secondary').text('Kapat').attr({
                        type: 'button',
                        'data-bs-dismiss': 'modal'
                    }).css({ "height": "35px", "line-height": "13px" }),
                    $('<button>').addClass('btn btn-primary btnSave').text('Kaydet').attr('type', 'button').css({ "height": "35px", "line-height": "13px" }).attr('disabled', 'true'), // Başlangıçta pasif durumda
                )
            )
        )
    );

    // Modalın içeriği ve form alanlarını hazırla
    var modalBody = modal.find('.modal-body');
    var row;
    columns.forEach(function (column, index) {
        if (column.render) return; // 'render' varsa bu sütunu atla

        // Her iki sütunda bir veya ilk sütunsa yeni satır başlat
        if (index % 2 === 0) {
            row = $('<div>').addClass('row');
            modalBody.append(row);
        }

        var inputType = column.type || 'text';
        var value = getNestedValue(rowData, column.field);
        var formGroup = $('<div>').addClass('form-group col-md-6');
        var label = $('<label>').text(column.header);
        var inputValue = (column.hideValue !== undefined && column.hideValue !== false) ? '' : (value || ''); // hideValue belirtilmemiş veya false ise veriyi göster
        var inputEditable = (processType === 'new') ? true : (column.editable !== false);
        var input = $('<input>').addClass('form-control').attr({
            type: inputType,
            name: column.field,
            value: inputValue,
            placeholder: column.header,
            disabled: !inputEditable
        }).on('change', function () {
            // Input alanı değiştiğinde kontrolü yap
            checkRequiredFields();
        });

        if (column.required) {
            // Eğer alan 'required' olarak işaretlendiyse, gerekli işareti koy
            input.attr('data-required', 'true');
        }

        if (column.type === 'datetime') {
            input.addClass('datetimepicker');
            var inputGroup = $('<div>').addClass('input-groupicon').append(input);
            var addonset = $('<div>').addClass('addonset').append(
                $('<img>').attr('src', '/assets/img/icons/calendars.svg').attr('alt', 'img')
            );
            inputGroup.append(addonset);
            formGroup.append(label, inputGroup);
        } else {
            formGroup.append(label, input);
        }
        row.append(formGroup);

        // Son sütunda yeni satırı başlat
        if ((index % 2 !== 0 && index === columns.length - 1) || index === columns.length - 1) {
            modalBody.append(row);
        }
    });

    // Eski modali kaldır ve yeni modali sayfaya ekle
    $('#modals').empty();
    $('#modals').append(modal);

    // datetimepicker'ı başlat
    $('.datetimepicker').datetimepicker({
        format: 'DD-MM-YYYY',
        icons: {
            up: "fas fa-angle-up",
            down: "fas fa-angle-down",
            next: 'fas fa-angle-right',
            previous: 'fas fa-angle-left'
        }
    });

    // Formu kontrol etmek için setInterval kullan
    var intervalId = setInterval(checkRequiredFields, 100);

    function checkRequiredFields() {
        var requiredFields = $('[data-required="true"]');
        var disableButton = false;
        requiredFields.each(function () {
            var value = $(this).val();
            if (!value) {
                $(this).css('border-color', 'red');
                disableButton = true;
            } else {
                $(this).css('border-color', ''); // Alan doluysa kırmızı rengi kaldır
            }
        });

        var saveButton = $('.btn-primary');
        if (disableButton) {
            saveButton.attr('disabled', 'true');
        } else {
            saveButton.removeAttr('disabled');
        }
    }

    // Modal kapatıldığında kontrolü durdur
    $('#dynamicModal').on('hidden.bs.modal', function () {
        clearInterval(intervalId);
        // 'Kaydet' düğmesine tıklama olay dinleyicisini kaldır
        $(document).off('click', '.btnSave');
    });

    // 'Kaydet' düğmesine tıklanıldığında event'i tetikle ve formData'yı iletecek
    $(document).on('click', '.btnSave', function () {
        var data = {};

        columns.forEach(function (column) {
            if (column.field != undefined) {
                var fieldName = column.field;
                var input = $('[name="' + fieldName + '"]')[0]; // İlk eşleşen elemanı al

                if (column.editable) {
                    setNestedValue(rowData, fieldName, input.value);
                    setNestedValue(data, fieldName, input.value);
                }
            }
        });

        if (trElement) {
            // trElement tanımlı ise bu kod bloğu çalışır
            trElement.data("rowData", rowData);
            event(data);
        } else {
            // trElement tanımlı değilse, hata işlemleri burada yapılabilir veya gerekirse bir hata mesajı gösterilebilir.
            event(data);
        }

        $('#dynamicModal').modal('hide');
    });

    // Modalı göster
    $('#dynamicModal').modal('show');
}

$.fn.rowData = function () {
    return this.data("rowData");
};


$(() => {
    setTimeout(function () {
        $('#global-loader');
        setTimeout(function () {
            $("#global-loader").fadeOut("slow");
        }, 100);
    }, 500);

});
