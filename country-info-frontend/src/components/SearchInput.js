import React from 'react';

// SearchInput component to handle the search input and filter type
const SearchInput = ({ searchTerm, handleSearchChange, filterType, handleFilterTypeChange }) => {
    return (
        <div className="search-input-container">
            <input
                type="text"
                className="search-input"
                placeholder={`Search for a ${filterType}...`}
                value={searchTerm}
                onChange={handleSearchChange}
            />
            <select className="filter-type-selector" value={filterType} onChange={handleFilterTypeChange}>
                <option value="country">Country</option>
                <option value="region">Region</option>
                <option value="subregion">Subregion</option>
            </select>
        </div>
    );
};

export default SearchInput;