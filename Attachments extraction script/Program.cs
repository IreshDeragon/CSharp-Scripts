using System;
using System.Collections.Generic;
using System.Threading;


using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using System.IO;

namespace ExtractionPJ
{
    class Program
    {
        static void Main(string[] args) // en entrée d'argument : 0=> Objet des mails à traiter ; 1=> chemin du répertoire d'enregistrement des pièces jointes ; 2=> Dossier d'archivage sur outlook
            //pour le chemin d'accès au répertoire d'enregistrement, il faut mêtre '\\' pour ignorer le caractère '\' qui transforme le caractère suivant en commande (ex \n qui produit un retour à la ligne)

        {
            using (var client = new ImapClient())
            {
                using (var cancel = new CancellationTokenSource())
                {
                    //Connexion au serveur Imap de chez microsoft outlook : pour d'autres hébergeur mail, voir les configuration requises pour le protocole Imap
                    client.Connect("outlook.office365.com", 993, true, cancel.Token);

                    client.Authenticate("anExample@mail.onmicrosoft.fr", "a super secured password", cancel.Token);

                    // Récupération de tous les mails de la boite!
                    var destinationFolder = client.GetFolder(Properties.Settings.Default.ArchiveMail);//(@args[2]); 
                    destinationFolder.Open(FolderAccess.ReadWrite);
                    System.Diagnostics.Debug.WriteLine(destinationFolder.Name);
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadWrite, cancel.Token);

                    Console.WriteLine("Total messages: {0}", inbox.Count);
                    Console.WriteLine("Recent messages: {0}", inbox.Recent);
                    System.Diagnostics.Debug.WriteLine("Total messages: {0}", inbox.Count);
                    System.Diagnostics.Debug.WriteLine("Recent messages: {0}", inbox.Recent);

                    // Parcours de tous les messages récupérés
                    var listUID = inbox.Fetch(0, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Size | MessageSummaryItems.Flags);

                    foreach (var item in listUID)
                    {
                        var message = inbox.GetMessage(item.UniqueId, cancel.Token);



                        if (message.Subject.Contains(Properties.Settings.Default.ObjetMail))//par exemple : si l'objet du mail contient "URGENT", alors on récupère la pièce jointe
                        {
                            foreach (MimePart attachment in message.Attachments)
                            {
                                string filePath = Properties.Settings.Default.CheminPJ +"\\"+ DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")+ message.Subject.ToString() +Guid.NewGuid().ToString().Substring(0, 9) + Path.GetExtension(attachment.FileName); //création du fichier dans le répertoire choisi en paramètre du programme
                                using (var stream = File.Create(filePath))
                                {
#pragma warning disable CS0618 // Le type ou le membre est obsolète
                                    attachment.ContentObject.DecodeTo(stream, cancel.Token);
#pragma warning restore CS0618 // Le type ou le membre est obsolète
                                }
                                
                            }
                            inbox.MoveTo(item.UniqueId,destinationFolder);
                            System.Diagnostics.Debug.WriteLine(message.MessageId);
                            Console.WriteLine("Subject: {0}", message.Subject);
                        }
                    }
                }
            }
        }
    }
}
