import axios from "axios";

const baseUrl = "https://webapireact.azurewebsites.net/api/"


//Use create api calls
export default {

    dCandidate(url = baseUrl + 'DonationCandidate/') {
        return {
            //Gets all data from the database
            fetchAll: () => axios.get(url),
            //Gets data by Id from the database
            fetchById: id => axios.get(url + id),
            //Creates new entry for donor
            create: newRecord => axios.post(url, newRecord),
            //Update the entry for donor
            update: (id, updateRecord) => axios.put(url + id, updateRecord),
            // Deletes data
            delete: id => axios.delete(url + id)
        }
    }
}