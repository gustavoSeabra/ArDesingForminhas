$(document).ready(function () {
    $('#gridProduto').DataTable({
        language: {
            url: "../Scripts/plugin/dataTables.Portuguese-Brasil.json"
        }
    });

    $('.btn').click(function (e) {
        if ($(this)[0].innerText !== " Cadastrar") {
            var url = urlDelete;
            var valor = $(this).attr("CommandArgument");

            url += "/" + valor

            swal({
                title: "Realmente deseja excluir o produto?",
                text: "Não será possível desfazer esta alteração!",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Sim",
                cancelButtonText: "Não",
                closeOnConfirm: false,
                closeOnCancel: true,
                showLoaderOnConfirm: true
            },
                function () {
                    $.post(url, function (data) {
                        var response = JSON.parse(data);
                        if (response.Status === 200) {
                            PreencheGrid();
                            swal("", "Produto excluido com sucesso.", "success");
                        }
                        else
                            swal("Ocorreu um erro ao excluir o produto.", "O erro foi: " + response.Mensagem, "error");
                    });
                });
        }
    });
});