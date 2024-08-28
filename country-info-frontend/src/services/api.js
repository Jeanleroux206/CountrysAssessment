import axios from 'axios';

export const fetchAllCountries = () => {
    return axios.get(`api/Country/all`);
};

export const fetchCountryByName = (name) => {
    return axios.get(`/api/Country/name/${name}`);
};

export const fetchCountryByCode = (code) => {
    return axios.get(`/api/Country/code/${code}`);
};

export const fetchRegion = (region) => {
    return axios.get(`/api/region/${region}`);
};

export const fetchSubregion = (subregion) => {
    return axios.get(`/api/subregion/${subregion}`);
};