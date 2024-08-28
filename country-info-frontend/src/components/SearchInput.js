import React from 'react';

const SearchInput = ({ searchTerm, handleSearchChange }) => {
    return (
        <input
            type="text"
            placeholder="Search countries..."
            value={searchTerm}
            onChange={handleSearchChange}
            className="search-input"
        />
    );
};

export default SearchInput;