var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "discountPrice", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                     <div class="m-75 btn-group" role="group">
                        <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>編輯</a>

                        <a onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-x-square-fill"></i>刪除</a>
                    </div>
                     `
                },
                "width": "15%"
            }
        ]
    });

}



function Delete(url) {
    Swal.fire({
        title: '你確定要刪除嗎?',
        text: "刪除後即不可恢復",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '確定刪除',
        cancelButtonText: "取消"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toasrt.success(data.message);
                    } else {
                        toasrt.error(data.message);
                    }
                }
            })
        }
    })
}