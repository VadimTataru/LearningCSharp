using System;
using System.IO;
using CsharpPhonebook.Models;

namespace CsharpPhonebook
{
    public class Phonebook
    {
        private const string filepath = "phonebook.txt";

        #region singleton
        private static Phonebook? instance;

        private Phonebook()
        {
            if(!File.Exists(filepath))
                File.Create(filepath);

        }

        public static Phonebook GetInstance()
        {
            if (instance == null)
                instance = new Phonebook();
            return instance;
        }
        #endregion

        #region CRUD
        public async Task<bool> CreateContactAsync(Contact contact)
        {
            var contacts = await ReadContactAsync();

            if (contacts.Contains(contact))
                return false;

            using (StreamWriter sw = new StreamWriter(filepath, true))
            {
                await sw.WriteLineAsync($"{contact}");
            }
            return true;
        }

        public async Task<List<Contact>> ReadContactAsync()
        {
            var contacts = new List<Contact>();
            using (StreamReader reader = new StreamReader(filepath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line == null || line == String.Empty)
                        return contacts;
                    var contactData = line.Split(":");
                    contacts.Add(new Contact(
                        contactData[0],
                        contactData[1]
                        ));
                }
            }
            return contacts;
        }

        public async Task UpdateContact(int id, Contact contact)
        {
            var contacts = await ReadContactAsync();
            contacts[id] = contact;
            await RewriteFile(contacts);
        }

        public async Task DeleteContact(int contactId)
        {
            var contacts = await ReadContactAsync();
            contacts.RemoveAt(contactId);
            await RewriteFile(contacts);
        }
        #endregion

        private async Task RewriteFile(List<Contact> contacts)
        {
            using (StreamWriter sw = new StreamWriter(filepath, false))
            {
                foreach (var c in contacts)
                {
                    await sw.WriteLineAsync($"{c}");

                }
            }
        }
    }
}

