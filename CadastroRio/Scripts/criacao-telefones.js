$(function () {
    var qtdTelefones = 0;

    $("#btn-add-telefone").click(function (e) {
        e.preventDefault();

        $("button").prop("disabled", false);
        document.getElementById("cadastrar").className = "btn btn-success btn-lg" ;

        var blocoTelefone = '<div class="row">' +
            '    <div class="col-md-2">' +
            '        <input type="number" min="11" max="99" maxlength="2" name="ddd[' + qtdTelefones + ']" maxlength="2" placeholder="DDD" class="form-control txt-ddd" />' +
            '    </div>' +
            '    <div class="col-md-6">' +
            '        <input type="text" maxlength="9" name="numero[' + qtdTelefones + ']" placeholder="Número" class="form-control txt-numero" />' +
            '    </div>' +
            '    <div class="col-md-1">' +
            '        <button class="btn btn-danger btn-remover-telefone">' +
            '            <span class="glyphicon glyphicon-trash"></span>' +
            '        </button>' +
            '    </div>' +
            '</div>';
        
        $("#div-telefones").append(blocoTelefone);

        qtdTelefones++;
        document.getElementById("qtdtel").value = qtdTelefones;
    });

    $("#div-telefones").on("click", ".btn-remover-telefone", function (e) {
        e.preventDefault();

        $(this).parent().parent().remove();

        qtdTelefones--;
        
        $("#div-telefones .row").each(function (indice, elemento) {
            $(elemento).find(".txt-ddd").attr("name", "ddd[" + indice + "]");
            $(elemento).find(".txt-numero").attr("name", "numero[" + indice + "].Numero");
        });
        document.getElementById("qtdtel").value = qtdTelefones;
        if (qtdTelefones <= 0) {
            $("#cadastrar").prop("disabled", true);
            document.getElementById("cadastrar").className = "btn btn-success disabled btn-lg";
        }
    });
});