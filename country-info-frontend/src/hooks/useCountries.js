import { useState, useEffect } from 'react';
import { fetchAllCountries } from '../services/api';

const useCountries = () => {
    const [countries, setCountries] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchAllCountries()
            .then(response => setCountries(response.data))
            .catch(error => setError('Error fetching countries.'));
    }, []);

    return { countries, error };
};

export default useCountries;