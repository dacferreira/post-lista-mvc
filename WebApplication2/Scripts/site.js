$(document).ready(function () {
    var $modal = $('#editor-modal'),
	$editor = $('#editor'),
	$editorTitle = $('#editor-title'),
	ft = FooTable.init('#editing-example', {
	    editing: {
	        enabled: true,
	        addRow: function () {
	            $modal.removeData('row');
	            $editor[0].reset();
	            $editorTitle.text('Add a new row');
	            $modal.modal('show');
	        },
	        editRow: function (row) {
	            var values = row.val();
	            $editor.find('#id').val(values.id);
	            $editor.find('#logradouro').val(values.logradouro);
	            $editor.find('#cep').val(values.cep);
	            $editor.find('#numero').val(values.numero);

	            $modal.data('row', row);
	            $editorTitle.text('Edit row #' + values.id);
	            $modal.modal('show');
	        },
	        deleteRow: function (row) {
	            if (confirm('Are you sure you want to delete the row?')) {
	                row.delete();
	            }
	        }
	    }
	}),
	uid = 1;

    $editor.on('submit', function (e) {
        if (this.checkValidity && !this.checkValidity()) return;
        e.preventDefault();

        var model = {
            IdEndereco: $editor.find('#id').val() == "" ? uid : $editor.find('#id').val(),
            Logradouro: $editor.find('#logradouro').val(),
            CEP: $editor.find('#cep').val(),
            Numero: $editor.find('#numero').val()
        };

        $.ajax({
            url: "/Revenda/CadastroEndereco",
            contentType: 'application/json',
            type: "POST",
            data: JSON.stringify({ model: model }),
            cache: false,
            success: function (html) {
                $("#InformacaoEndereco").html(html);

                if ($editor.find('#id').val() == "")
                    uid++;

                limparCamposModal();
                $modal.modal('hide');
            }
        });
    });

    $("#fecharModal").bind("click", function () {
        limparCamposModal();
        $modal.modal('hide');
    });

    $(document).on("click", ".editarEndereco", function () {
        var rowId = $(this).data("row-id");

        var id = $(this).closest("tr").find("td#Id" + rowId).text().trim();
        var logradouro = $(this).closest("tr").find("td#Logradouro" + rowId).text().trim();
        var cep = $(this).closest("tr").find("td#Cep" + rowId).text().trim();
        var numero = $(this).closest("tr").find("td#Numero" + rowId).text().trim();

        $editor.find('#id').val(id);
        $editor.find('#logradouro').val(logradouro);
        $editor.find('#cep').val(cep);
        $editor.find('#numero').val(numero);

        //$modal.data('row', row);
        $editorTitle.text('Edit row #');
        $modal.modal('show');
    });

    $(document).on("click", ".removerEndereco", function () {
        var rowId = $(this).data("row-id");

        var id = $(this).closest("tr").find("td#Id" + rowId).text().trim();
        $.ajax({
            url: "/Revenda/ExcluirEndereco",
            contentType: 'application/json',
            type: "GET",
            data: { id: id },
            cache: false,
            success: function (html) {
                $("#InformacaoEndereco").html(html);
            }
        });
    });

    function limparCamposModal() {
        $("#id").val("");
        $("#logradouro").val("");
        $("#cep").val("");
        $("#numero").val("");
    }

    $("#CNPJ").on("blur", function() {
        $.ajax({
            type: 'GET',
            //url: 'https://www.receitaws.com.br/v1/cnpj/25042004000163/',
            url: 'https://viacep.com.br/ws/23585-440/json/',
            dataType: 'json',
            cache: false,
            async: true,
            success: function(data) {
                alert(data);
            },
            complete: function(date) {
                alert("complet", date);
            },
            error: function() {
                alert("erro na tentativa de obter o captcha");
            }
        });
    });
});