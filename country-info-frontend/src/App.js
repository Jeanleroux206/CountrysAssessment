import React, { useState } from 'react';
import './App.css';
import CountryTable from './components/CountryTable';
import DetailSection from './components/DetailSection';
import Pagination from './components/Pagination';
import RecordsPerPageSelector from './components/RecordsPerPageSelector';
import SearchInput from './components/SearchInput';
import useCountries from './hooks/useCountries';
import usePagination from './hooks/usePagination';
import { fetchCountryByName, fetchCountryByCode, fetchRegion, fetchSubregion } from './services/api';

function App() {
    const { countries, error: countriesError } = useCountries();
    const { currentPage, itemsPerPage, setItemsPerPage, paginate } = usePagination(50);
    const [selectedCountry, setSelectedCountry] = useState(null);
    const [selectedRegion, setSelectedRegion] = useState(null);
    const [selectedSubregion, setSelectedSubregion] = useState(null);
    const [error, setError] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');

    const handleCountryClick = (identifier, type) => {
        const fetchCountry = type === 'code' ? fetchCountryByCode : fetchCountryByName;

        fetchCountry(identifier)
            .then(response => {
                const countryData = response.data;
                setSelectedCountry(countryData);
                setSelectedRegion(null);
                setSelectedSubregion(null);
                setError(null);
            })
            .catch(error => setError('Failed to fetch country data. Please try again.'));
    };

    const handleRegionClick = (region) => {
        fetchRegion(region)
            .then(response => {
                setSelectedRegion(response.data);
                setSelectedCountry(null);
                setSelectedSubregion(null);
                setError(null);
            })
            .catch(error => setError('Failed to fetch region data. Please try again.'));
    };

    const handleSubregionClick = (subregion) => {
        fetchSubregion(subregion)
            .then(response => {
                setSelectedSubregion(response.data);
                setSelectedCountry(null);
                setSelectedRegion(null);
                setError(null);
            })
            .catch(error => setError('Failed to fetch subregion data. Please try again.'));
    };

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
        paginate(1); // Reset to first page on search
    };

    const handleRecordsPerPageChange = (event) => {
        setItemsPerPage(Number(event.target.value));
        paginate(1); // Reset to first page on change
    };

    const filteredCountries = countries.filter(country =>
        country.name.common.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const indexOfLastCountry = currentPage * itemsPerPage;
    const indexOfFirstCountry = indexOfLastCountry - itemsPerPage;
    const currentCountries = filteredCountries.slice(indexOfFirstCountry, indexOfLastCountry);

    return (
        <div className="App">
            <h1>Country Information</h1>
            <SearchInput searchTerm={searchTerm} handleSearchChange={handleSearchChange} />
            <div className="content-container">
                <div className="table-container">
                    <CountryTable
                        countries={currentCountries}
                        handleCountryClick={handleCountryClick}
                        handleRegionClick={handleRegionClick}
                        handleSubregionClick={handleSubregionClick}
                    />
                </div>
                {(selectedCountry || selectedRegion || selectedSubregion) && (
                    <DetailSection
                        selectedCountry={selectedCountry}
                        selectedRegion={selectedRegion}
                        selectedSubregion={selectedSubregion}
                        handleCountryClick={handleCountryClick}
                        handleRegionClick={handleRegionClick}
                        handleSubregionClick={handleSubregionClick}
                    />
                )}
            </div>
            <Pagination
                countriesPerPage={itemsPerPage}
                totalCountries={filteredCountries.length}
                paginate={paginate}
                currentPage={currentPage}
            />
            <RecordsPerPageSelector
                countriesPerPage={itemsPerPage}
                handleRecordsPerPageChange={handleRecordsPerPageChange}
            />
            {error && <p className="error">{error}</p>}
        </div>
    );
}

export default App;