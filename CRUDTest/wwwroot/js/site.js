// Открытие модального окна для просмотра сотрудника
$('.order_tr').on('click', function () {
    let orderNumber = $(this).attr('order-number')
    let ProviderId = $(this).attr('provider-id')
    $.ajax({
        url: `/Home/ShowOrder`,
        method: "GET",
        data: { orderNumber: orderNumber, ProviderId: ProviderId },
        success: function (html) {
            console.log(html)
            $('#show_order .modal-dialog').html(html);
        },
        error: (res) => {
            console.log(res)
        }
    });
});


function FilterOrders() {
    let orderNumber = JSON.stringify($("#select_number").val());
    let itemName = JSON.stringify($("#select_item_name").val());
    let itemUnit = JSON.stringify($("#select_item_unit").val());
    let providerName = JSON.stringify($("#select_provider").val());
    let timeBeg = $("#filter_date_beg").val();
    let timeEnd = $("#filter_date_end").val();
    console.log(orderNumber)
    console.log(itemName)
    console.log(itemUnit)
    console.log(providerName)
    $.ajax({
        url: `/Home/FilterOrder`,
        method: "GET",
        data: { orderNumber: orderNumber, itemName: itemName, itemUnit: itemUnit, timeBeg: timeBeg, timeEnd: timeEnd, providerName: providerName },
        success: function (html) {
            $('tbody').html(html);
        },
        error: (res) => {
            console.log(res)
        }
    });
}


function AddRow() {
    let tbody = $("tbody")[0];
    let row = document.createElement('tr');
    let data_1 = document.createElement('td');    
    let data_2 = document.createElement('td');
    let data_3 = document.createElement('td');
    let data_4 = document.createElement('td');

    data_1.innerHTML = '<input name="name" required autocomplete="off" />'
    data_2.innerHTML = '<input name="quantity" type="number" step="0.001" required autocomplete="off" />'
    data_3.innerHTML = '<input name="unit" required autocomplete="off" />'
    data_4.innerHTML = '<button class="btn btn-secondary" type="button" onclick="RemoveRow(this)">Удалить</button>'

    row.appendChild(data_1);
    row.appendChild(data_2);
    row.appendChild(data_3);
    row.appendChild(data_4);
    tbody.appendChild(row);
}


function RemoveRow(el) {
    let p = $(el);
    p.parents().eq(1).remove();
}

$('#add_form').on('submit', function (e) {
    e.preventDefault();    
    $.ajax({
        url: `/Home/NewOrder`,
        method: "POST",
        data: { form: decodeURI($(this).serialize()) },
        success: function (success) {
            if (success == 'true') {
                window.location.href = '/Home/Index';
            }
            else {
                let err = $(`#add_err`)
                err.toggleClass('d-none', false)
                err.html(success)
            }
        },
        error: (res) => {
            console.log(res)
        }
    });
})

$('#edit_form').on('submit', function (e) {
    e.preventDefault();
    var orderNumber = $('#edit_form').attr('orderNumber')
    var providerId = $('#edit_form').attr('providerId')
    $.ajax({
        url: `/Home/EditOrder`,
        method: "POST",
        data: { form: decodeURI($(this).serialize()), orderNumber: orderNumber, providerId: providerId },
        success: function (success) {
            if (success == 'true') {
                window.location.href = '/Home/Index';
            }
            else {
                let err = $(`#edit_err`)
                err.toggleClass('d-none', false)
                err.html(success)
            }
        },
        error: (res) => {
            console.log(res)
        }
    });
})