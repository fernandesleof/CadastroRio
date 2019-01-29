using CadastroRio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CadastroRio.Controllers
{
    public class ClienteController : Controller
    {

        // GET: Cliente

        //Listar todos os clientes
        public ActionResult Listar()
        {
            using(ClienteModel model=new ClienteModel())
            {
                List<Cliente> lista = model.Listar();
                return View(lista);
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        //Cadastrar Cliente
        public ActionResult Create(FormCollection form)
        {
            int idcliente = 0;
            Cliente cliente = new Cliente();
            Telefone telefone = new Telefone();
            TelefoneModel telmodel = new TelefoneModel();

            cliente.cpfCliente = form["cpf"];
            cliente.dataNascimentoCliente = Convert.ToDateTime(form["datanascimento"] + " 00:00");
            cliente.generoCliente = form["genero"];
            cliente.nomeCliente = form["nome"];

            ClienteModel model = new ClienteModel();
            idcliente = model.CadastroCliente(cliente);

            for (int contador = 0; contador < Convert.ToInt32(form["qtdtel"]); contador++)
            {
                {
                    telefone.idCliente = idcliente;
                    telefone.ddd = form["ddd["+contador+"]"];
                    telefone.numero = form["numero["+contador+"]"];
                    telmodel.CadastroTelefone(telefone);
                }
            }
            return RedirectToAction("Listar");
        }
        public ActionResult Sucesso()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ClienteModel model = new ClienteModel();
            Cliente cliente=model.GetCliente(id);
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            ClienteModel model = new ClienteModel();
            TelefoneModel modeltel = new TelefoneModel();
            modeltel.ExcluirTelCliente(Convert.ToInt32(id));
            model.Excluir(Convert.ToInt32(id));
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public void RemoverTelefone(int id)
        {
            TelefoneModel model = new TelefoneModel();
            model.Excluir(Convert.ToInt32(id));
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ClienteModel model = new ClienteModel();
            Cliente cliente = model.GetCliente(id);

            //Buscando os dados de cliente
            TelefoneModel modeltel = new TelefoneModel();
            cliente.Telefones= modeltel.Listar(id);

            return View(cliente);

        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form,[Bind] Cliente cliente)
        {
            int contador = 0;
            Telefone telefone = new Telefone();
            TelefoneModel telmodel = new TelefoneModel();
            cliente.idCliente =Convert.ToInt32(form["id"]);
            cliente.cpfCliente = form["cpf"];
            cliente.dataNascimentoCliente = Convert.ToDateTime(form["datanascimento"] + " 00:00");
            cliente.generoCliente = form["genero"];
            cliente.nomeCliente = form["nome"];

            //para alterar telefones inseridos
            for (contador=0; contador < Convert.ToInt32(form["qtdtelAlt"]); contador++)
            {
                {
                    telefone.idTelefone = Convert.ToInt32(form["idtelefone [" + contador + "]"]);
                    telefone.idCliente = Convert.ToInt32(form["id"]);
                    telefone.ddd = form["ddd [" + contador + "]"];
                    telefone.numero = form["numero [" + contador + "]"];
                    telmodel.AlterarTelefone(telefone);
                }
            }

            //para inserir se existir novos na tela
            for (int contadori = 0; contadori < Convert.ToInt32(form["qtdtel"]); contadori++)
            {
                {
                    telefone.idCliente = Convert.ToInt32(form["id"]);
                    telefone.ddd = form["ddd [" + contador + "]"];
                    telefone.numero = form["numero [" + contador + "]"];
                    telmodel.CadastroTelefone(telefone);
                    contador++;
                }
            }

            ClienteModel model = new ClienteModel();
            model.Alterar(cliente);
            return RedirectToAction("Listar");

        }

    }



}