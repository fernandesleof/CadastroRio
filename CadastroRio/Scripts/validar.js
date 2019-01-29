$(document).ready(function(){
	$("#form").validate({
		rules:{
			nome:{
				required: true,
				minlength: 3
			},
			email:{
				required: true,
				email: true
			},
			idade:{
				required: true,
				number: true,
				range: [18, 90]
			}
		},
		messages:{
			nome:{
				required: "Digite um nome!",
				minlength: "No minimo 3 letras"
			},
			email:{
				required: "Digite o email!",
				email: "Digite um email valido!"
			}
		}
	});
});

























































































