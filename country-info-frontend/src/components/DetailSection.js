import React from 'react';

// DetailSection component to display details of the selected country, region, or subregion
const DetailSection = ({ selectedCountry, selectedRegion, selectedSubregion, handleCountryClick, handleRegionClick, handleSubregionClick }) => {
    return (
        <div className="detail-container">
            <div className="detail-section">
                {selectedCountry && (
                    <>
                        <h2>{selectedCountry.name.common}</h2>
                        <p>Capital: {selectedCountry.capital ? selectedCountry.capital[0] : 'N/A'}</p>
                        <p>Population: {selectedCountry.population.toLocaleString()}</p>
                        <p>Currencies: {selectedCountry.currencies ? Object.values(selectedCountry.currencies).map(currency => currency.name).join(', ') : 'N/A'}</p>
                        <p>Languages: {selectedCountry.languages ? Object.values(selectedCountry.languages).join(', ') : 'N/A'}</p>
                        <p>Borders: {selectedCountry.borders ? selectedCountry.borders.map(border => (
                            <span key={border} onClick={() => handleCountryClick(border, 'code')} style={{cursor: 'pointer', color: 'blue', textDecoration: 'underline'}}>
                                {border}
                            </span>
                        )).reduce((prev, curr) => [prev, ', ', curr]) : 'N/A'}</p>
                    </>
                )}
                {selectedRegion && (
                    <div className="detail-section">
                        <h2>{selectedRegion.name}</h2>
                        <p>Total Population: {selectedRegion.population.toLocaleString()}</p>
                        <p>Countries: {selectedRegion.countries.map(country => (
                            <span key={country} onClick={() => handleCountryClick(country, 'name')} style={{cursor: 'pointer', color: 'blue', textDecoration: 'underline'}}>
                                {country}
                            </span>
                        )).reduce((prev, curr) => [prev, ', ', curr])}</p>
                        <p>Subregions: {selectedRegion.subregions.map(subregion => (
                            <span key={subregion} onClick={() => handleSubregionClick(subregion)} style={{cursor: 'pointer', color: 'blue', textDecoration: 'underline'}}>
                                {subregion}
                            </span>
                        )).reduce((prev, curr) => [prev, ', ', curr])}</p>
                    </div>
                )}
                {selectedSubregion && (
                    <div className="detail-section">
                        <h2>{selectedSubregion.name}</h2>
                        <p>Total Population: {selectedSubregion.population.toLocaleString()}</p>
                        <p>Region: <span onClick={() => handleRegionClick(selectedSubregion.region)} style={{cursor: 'pointer', color: 'blue', textDecoration: 'underline'}}>{selectedSubregion.region}</span></p>
                        <p>Countries: {selectedSubregion.countries.map(country => (
                            <span key={country} onClick={() => handleCountryClick(country, 'name')} style={{cursor: 'pointer', color: 'blue', textDecoration: 'underline'}}>
                                {country}
                            </span>
                        )).reduce((prev, curr) => [prev, ', ', curr])}</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default DetailSection;