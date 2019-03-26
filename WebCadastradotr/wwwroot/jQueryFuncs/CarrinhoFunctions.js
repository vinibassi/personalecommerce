$("#removeItem").on("click", function () {
    var subtracao = Number($("#produtoQuantidade").val()) - 1;
    $("#produtoQuantidade").val(subtracao);
}
);
$("#addItem").on("click", function () {
    var soma = Number($("#produtoQuantidade").val()) + 1;
    $("#produtoQuantidade").val(soma);
}
);