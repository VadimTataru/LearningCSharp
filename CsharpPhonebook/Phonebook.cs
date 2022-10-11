using System;
using System.IO;
using CsharpPhonebook.Models;

namespace CsharpPhonebook
{
    /// <summary>
    /// Главный класс
    /// </summary>
    public class Phonebook
    {
        private const string filepath = "phonebook.txt";

        /// <summary>
        /// Синглтон
        /// </summary>
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
        /// <summary>
        /// Ассинхронный метод записи нового контакта
        /// </summary>
        /// <param name="contact">На вход подаётся модель Contact</param>
        /// <returns>True - контакт записан, False - контакт не записан</returns>
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

        /// <summary>
        /// Ассинхронное получение контактов из файла
        /// </summary>
        /// <returns>Коллекция контактов</returns>
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

        /// <summary>
        /// Ассинхронный метод изменения контакта
        /// </summary>
        /// <param name="id">Идентификатор контакта</param>
        /// <param name="contact">Заменяющие данные контакта</param>
        /// <returns>Nothing</returns>
        public async Task UpdateContact(int id, Contact contact)
        {
            var contacts = await ReadContactAsync();
            contacts[id] = contact;
            await RewriteFile(contacts);
        }

        /// <summary>
        /// Удаляет контакт по идентификатору
        /// </summary>
        /// <param name="contactId">Идентификатор контакта</param>
        /// <returns>Another one (nothing)</returns>
        public async Task DeleteContact(int contactId)
        {
            var contacts = await ReadContactAsync();
            contacts.RemoveAt(contactId);
            await RewriteFile(contacts);
        }
        #endregion

        /// <summary>
        /// Вспомогательный метод перезаписи всех контактов
        /// </summary>
        /// <param name="contacts">Список контактов</param>
        /// <returns>And another one (nothing)</returns>
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

