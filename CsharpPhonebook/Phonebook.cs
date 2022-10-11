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
                    var contactData = line.Split(":");
                    contacts.Add(new Contact(
                        contactData[0],
                        contactData[1]
                        ));
                }
            }
            return contacts;
        }

        public async void UpdateContact(int id, Contact contact)
        {
            var contacts = await ReadContactAsync();
            contacts[id] = contact;
            RewriteFile(contacts);
        }

        public async void DeleteContact(Contact contact)
        {
            var contacts = await ReadContactAsync();
            contacts.Remove(contact);
            RewriteFile(contacts);
        }
        #endregion

        private async void RewriteFile(List<Contact> contacts)
        {
            using (StreamWriter sw = new StreamWriter(filepath, true))
            {
                foreach (var c in contacts)
                {
                    await sw.WriteLineAsync($"{c}");
                }
            }
        }
    }
}

